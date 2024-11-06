using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Simplify.Scheduler.Job.Interfaces;

namespace Microsoft.AspNetCore.Builder
{
    public static class SimplifySchedulerJobBuilderExtension
    {
        /// <summary>
        /// Start SimplifySchedulerJob on the specified <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the service to.</param>
        /// <param name="assembly">The <see cref="Assembly"/> of service to be read to register jobs.</param>
        /// <returns>An instance of <see cref="IApplicationBuilder"/> after the operation has completed.</returns>
        public static IApplicationBuilder UseSimplifySchedulerJob(this IApplicationBuilder app, Assembly assembly)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var scheduler = app.ApplicationServices.GetService<ISchedulerService>();
            scheduler?.Start(assembly);
            return app;
        }
    }
}