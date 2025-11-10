using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Quartz.Spi;
using Simplify.Scheduler.Job;
using Simplify.Scheduler.Job.Extensions;
using Simplify.Scheduler.Job.Interfaces;
using Simplify.Scheduler.Job.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Dependency injection extensions that register Simplify Scheduler services.
    /// </summary>
    public static class SimplifySchedulerJobDIExtension
    {
        /// <summary>
        /// Adds all SimplifySchedulerJob dependencies to the given <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">Collection where the infrastructure services will be registered.</param>
        /// <param name="configuration">Application configuration used to bind the job options.</param>
        /// <param name="assembly">Assembly scanned to find job interfaces decorated with <see cref="JobServiceAttribute"/>.</param>
        /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddSimplifySchedulerJob(this IServiceCollection services
            , IConfiguration configuration, Assembly assembly)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IJobFactory, SchedulerJobFactory>();
            services.AddSingleton<ISchedulerService, SchedulerService>();

            foreach (var service in assembly.GetJobTypeServices())
            {
                if (service.GetJobTypeAttribute() is JobServiceAttribute attrOptions)
                    services.AddConfigureOptions(configuration, attrOptions.TypeOptions);

                services.AddTransient(service.GetJobTypeInterface(), service.GetJobTypeService());
            }

            return services;
        }

        /// <summary>
        /// Binds the concrete <see cref="JobOptions"/> type to its configuration section at runtime.
        /// </summary>
        /// <param name="services">Service collection receiving the configuration binding.</param>
        /// <param name="configuration">Configuration root used to resolve the section.</param>
        /// <param name="option">Concrete type derived from <see cref="JobOptions"/>.</param>
        private static IServiceCollection AddConfigureOptions(this IServiceCollection services
            , IConfiguration configuration
            , Type option)
        {
            if (option == null) throw new ArgumentNullException(nameof(option));

            var section = configuration.GetSection(option.Name);
            var configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(m =>
                    m.Name == nameof(OptionsConfigurationServiceCollectionExtensions.Configure)
                    && m.IsGenericMethodDefinition
                    && m.GetParameters().Length == 2
                    && m.GetParameters()[1].ParameterType == typeof(IConfiguration));

            configureMethod
                .MakeGenericMethod(option)
                .Invoke(null, new object[] { services, section });

            return services;
        }
    }
}
