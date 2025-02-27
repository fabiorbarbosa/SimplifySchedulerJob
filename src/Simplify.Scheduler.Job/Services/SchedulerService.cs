using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Simplify.Scheduler.Job.Extensions;
using Simplify.Scheduler.Job.Interfaces;

namespace Simplify.Scheduler.Job.Services;

internal sealed class SchedulerService : ISchedulerService
{
    private readonly IJobFactory _jobFactory;
    private readonly IConfiguration _configuration;

    public SchedulerService(IJobFactory jobFactory, IConfiguration configuration)
    {
        _jobFactory = jobFactory;
        _configuration = configuration;
    }

    public void Start(Assembly assembly)
    {
        var schedulerProps = new NameValueCollection
        {
            ["quartz.scheduler.instanceId"] = "AUTO",
            ["quartz.scheduler.instanceName"] = assembly.GetName().Name
        };

        IScheduler _scheduler = new StdSchedulerFactory(schedulerProps)
            .GetScheduler()
            .Result;
        _scheduler.JobFactory = _jobFactory;

        foreach (Type jobType in assembly.GetJobTypeServices())
        {
            if (jobType.GetJobTypeAttribute() is JobServiceAttribute jobAttr)
            {
                if (_configuration
                    .GetSection(jobAttr.TypeOptions.Name)
                    .Get(jobAttr.TypeOptions) is not JobOptions options)
                {
                    throw new InvalidOperationException($"JobOptions for {jobAttr.TypeOptions.Name} could not be found.");
                }

                if (jobType.GetJobDetail() is not IJobDetail jobService)
                {
                    throw new InvalidOperationException($"JobDetail for {jobType.Name} could not be found.");
                }

                _scheduler.AddJob(jobService, true);

                foreach (var cronExpression in options.CronExpressions.Select((v, i) => new { Index = i, Value = v }))
                {
                    ITrigger trigger = TriggerBuilder
                        .Create()
                        .WithIdentity(
                            string.Format(CultureInfo.InvariantCulture, "{0}_Trigger_{1}",
                                jobService.Key.Name,
                                cronExpression.Index),
                            string.Format(CultureInfo.InvariantCulture, "Trigger_Group_{0}",
                                jobService.Key.Name))
                        .WithCronSchedule(cronExpression.Value)
                        .ForJob(jobService)
                        .StartAt(!string.IsNullOrWhiteSpace(options.TimeZoneId)
                            ? TimeZoneInfo.ConvertTime(DateTime.Now,
                                TimeZoneInfo.FindSystemTimeZoneById(options.TimeZoneId))
                            : DateTime.Now)
                        .Build();

                    _scheduler.ScheduleJob(trigger);
                }
            }
        }

        _scheduler.Start();
    }
}
