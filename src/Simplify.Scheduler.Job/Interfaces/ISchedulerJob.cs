using Quartz;

namespace Simplify.Scheduler.Job.Interfaces;

#pragma warning disable S2326 // Unused type parameters should be removed
internal interface ISchedulerJob<T> : IJob where T : IJobService
#pragma warning restore S2326 // Unused type parameters should be removed
{

}
