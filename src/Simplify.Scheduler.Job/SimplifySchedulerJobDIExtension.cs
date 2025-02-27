using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Quartz.Spi;
using Simplify.Scheduler.Job;
using Simplify.Scheduler.Job.Extensions;
using Simplify.Scheduler.Job.Interfaces;
using Simplify.Scheduler.Job.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class SimplifySchedulerJobDIExtension
{
    /// <summary>
    /// Add dependencies injection service of SimplifySchedulerJob to the
    /// specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddSimplifySchedulerJob(this IServiceCollection services
        , IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);

        var assembly = Assembly.GetCallingAssembly();

        services.AddTransient<IJobFactory, SchedulerJobFactory>();
        services.AddSingleton<ISchedulerService, SchedulerService>();

        foreach (Type service in assembly.GetJobTypeServices())
        {
            if (service.GetJobTypeAttribute() is JobServiceAttribute attrOptions)
            {
                services.AddConfigureOptions(configuration, attrOptions.TypeOptions);
            }

            services.AddTransient(service.GetJobTypeInterface(), service.GetJobTypeService());
        }

        return services;
    }

    private static IServiceCollection AddConfigureOptions<T>(this IServiceCollection services
        , IConfiguration configuration
        , T option) where T : Type
            => services.Configure<T>(opt => configuration.GetSection(option.Name));
}
