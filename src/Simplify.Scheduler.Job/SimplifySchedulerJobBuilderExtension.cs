using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Simplify.Scheduler.Job.Interfaces;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// ASP.NET Core middleware extensions that bootstrap the Simplify Scheduler pipeline.
    /// </summary>
    public static class SimplifySchedulerJobBuilderExtension
    {
        /// <summary>
        /// Starts SimplifySchedulerJob on the specified <see cref="IApplicationBuilder"/>.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> that will host the scheduled jobs.</param>
        /// <param name="assembly">Assembly that contains the job interfaces decorated with <c>Simplify.Scheduler.Job.JobServiceAttribute</c>.</param>
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
