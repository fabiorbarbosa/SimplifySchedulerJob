using System;
using System.Reflection;

namespace Simplify.Scheduler.Job.Extensions;

internal static class JobServiceAttributeExtension
{
    public static JobServiceAttribute GetJobTypeAttribute(this Type type)
        => type
            .GetCustomAttribute<JobServiceAttribute>(false);
}
