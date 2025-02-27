using System;
using Quartz;

namespace Simplify.Scheduler.Job.Extensions;

internal static class JobDetailExtension
{
    public static IJobDetail GetJobDetail(this Type jobType)
    {
        Type job = jobType.GetJobTypeInterface();
        return JobBuilder
            .Create(job)
            .WithIdentity(jobType.Name)
            .Build();
    }
}
