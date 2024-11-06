using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Simplify.Scheduler.Job.Interfaces;

namespace Simplify.Scheduler.Job.Services
{
    internal class SchedulerJob<T> : ISchedulerJob<T> where T : IJobService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SchedulerJob<T>> _logger;

        public SchedulerJob(IServiceProvider serviceProvider, ILogger<SchedulerJob<T>> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = _serviceProvider.CreateScope();
            if (scope.ServiceProvider.GetService<T>() is T jobService)
            {
                await jobService.ExecuteJobAsync();
                _logger.LogInformation("Job '{0}' executed at {1}!", context.JobDetail.Key.Name, DateTime.Now);
            }
            else
                _logger.LogError("JobService '{0}' not found!", nameof(T));
        }
    }
}