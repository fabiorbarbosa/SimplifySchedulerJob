using System;
using Quartz;
using Quartz.Spi;
using Simplify.Scheduler.Job.Interfaces;

namespace Simplify.Scheduler.Job.Services
{
    internal class SchedulerJobFactory : ISchedulerJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SchedulerJobFactory(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
            => _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}