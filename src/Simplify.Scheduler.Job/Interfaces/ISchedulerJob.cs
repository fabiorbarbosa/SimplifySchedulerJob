using System;
using Quartz;

namespace Simplify.Scheduler.Job.Interfaces
{
    internal interface ISchedulerJob<T> : IJob where T : IJobService
    {

    }
}