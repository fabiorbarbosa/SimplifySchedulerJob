using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simplify.Scheduler.Job.Interfaces;
using Simplify.Scheduler.Job.Services;

namespace Simplify.Scheduler.Job.Extensions
{
    internal static class TypeExtension
    {
        public static Type GetJobTypeInterface(this Type type)
            => typeof(ISchedulerJob<>).MakeGenericType(type);

        public static Type GetJobTypeService(this Type type)
            => typeof(SchedulerJob<>).MakeGenericType(type);

        public static IEnumerable<Type> GetJobTypeServices(this Assembly assembly)
            => assembly
                .GetTypes()
                .Where(t => typeof(IJobService).IsAssignableFrom(t) && t.IsInterface);
    }
}