using System;
using System.Reflection;
using Quartz.Spi;
using Simplify.Scheduler.Job.Extensions;
using Simplify.Scheduler.Job.Interfaces;
using Simplify.Scheduler.Job.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SimplifySchedulerJobDIExtension
    {
        /// <summary>
        /// Add dependencies injection service of SimplifySchedulerJob to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="assembly">The <see cref="Assembly"/> of service to be read to register jobs.</param>
        /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSimplifySchedulerJob(this IServiceCollection services, Assembly assembly)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IJobFactory, SchedulerJobFactory>();
            services.AddSingleton<ISchedulerService, SchedulerService>();

            foreach (var service in assembly.GetJobTypeServices())
                services.AddTransient(service.GetJobTypeInterface(), service.GetJobTypeService());

            return services;
        }
    }
}