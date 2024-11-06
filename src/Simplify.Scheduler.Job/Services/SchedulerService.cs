using System.Reflection;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Simplify.Scheduler.Job.Attributes;
using Simplify.Scheduler.Job.Extensions;
using Simplify.Scheduler.Job.Interfaces;

namespace Simplify.Scheduler.Job.Services
{
    internal class SchedulerService : ISchedulerService
    {
        private readonly IScheduler _scheduler;
        private readonly IConfiguration _configuration;

        public SchedulerService(IJobFactory jobFactory, IConfiguration configuration)
        {
            _scheduler = new StdSchedulerFactory()
                .GetScheduler()
                .Result;
            _scheduler.JobFactory = jobFactory;
            _configuration = configuration;
        }

        public void Start(Assembly assembly)
        {
            foreach (var job in assembly.GetJobTypeServices())
            {
                if (job.GetJobTypeAttribute() is JobServiceAttribute jobAttr)
                {
                    if (_configuration[jobAttr?.CronJobSchedule] is string cron)
                    {
                        var jobService = job.GetJobDetail();
                        var trigger = TriggerBuilder.Create()
                            .StartNow()
                            .WithCronSchedule(cron)
                            .Build();
                        _scheduler.ScheduleJob(jobService, trigger);
                    }
                }
            }
            _scheduler.Start();
        }
    }
}