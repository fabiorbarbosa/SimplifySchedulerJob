using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simplify.Scheduler.Job.Interfaces;
using Simplify.Scheduler.Job.Services;

namespace Simplify.Scheduler.Job.Extensions
{
    internal static class JobTypeExtension
    {
        public static Type GetJobTypeInterface(this Type jobService)
            => typeof(ISchedulerJob<>).MakeGenericType(jobService);

        public static Type GetJobTypeService(this Type jobService)
            => typeof(SchedulerJob<>).MakeGenericType(jobService);

        public static IEnumerable<Type> GetJobTypeServices(this Assembly assembly)
            => assembly
                .GetTypes()
                .Where(t => typeof(IJobService).IsAssignableFrom(t)
                            && t.IsInterface);
    }
}