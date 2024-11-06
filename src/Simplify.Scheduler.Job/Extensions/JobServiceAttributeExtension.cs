using System;
using System.Reflection;
using Simplify.Scheduler.Job.Attributes;

namespace Simplify.Scheduler.Job.Extensions
{
    internal static class JobServiceAttributeExtension
    {
        public static JobServiceAttribute? GetJobTypeAttribute(this Type type)
            => type
                .GetCustomAttribute(typeof(JobServiceAttribute), false) as JobServiceAttribute;
    }
}