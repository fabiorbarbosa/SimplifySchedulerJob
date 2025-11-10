using System.Collections.Specialized;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Simplify.Scheduler.Job.Extensions;
using Simplify.Scheduler.Job.Interfaces;

namespace Simplify.Scheduler.Job.Services
{
    internal class SchedulerService : ISchedulerService
    {
        private readonly ILogger<SchedulerService> _logger;
        private readonly IJobFactory _jobFactory;
        private readonly IConfiguration _configuration;

        public SchedulerService(ILogger<SchedulerService> logger, IJobFactory jobFactory, IConfiguration configuration)
        {
            _logger = logger;
            _jobFactory = jobFactory;
            _configuration = configuration;
        }

        public void Start(Assembly assembly)
        {
            var schedulerProps = new NameValueCollection();
            schedulerProps["quartz.scheduler.instanceId"] = "AUTO";
            schedulerProps["quartz.scheduler.instanceName"] = assembly.GetName().Name;

            var _scheduler = new StdSchedulerFactory(schedulerProps)
                .GetScheduler()
                .Result;
            _scheduler.JobFactory = _jobFactory;

            foreach (var job in assembly.GetJobTypeServices())
            {
                if (job.GetJobTypeAttribute() is JobServiceAttribute jobAttr)
                {
                    var options = _configuration
                        .GetSection(jobAttr.TypeOptions.Name)
                        .Get(jobAttr.TypeOptions) as JobOptions ?? null;

                    if (options != null)
                    {
                        var jobService = job.GetJobDetail();
                        var trigger = TriggerBuilder.Create()
                            .StartNow()
                            .WithCronSchedule(options.CronExpression)
                            .Build();

                        _scheduler.ScheduleJob(jobService, trigger);
                        _logger.LogInformation("Scheduled job: {0} with Cron: {1}", job.Name, options.CronExpression);
                    }
                    else
                        _logger.LogWarning("Job options for {0} not found.", job.Name);
                }
            }

            _scheduler.Start();
        }
    }
}
