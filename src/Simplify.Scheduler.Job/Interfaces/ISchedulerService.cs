using System.Reflection;

namespace Simplify.Scheduler.Job.Interfaces
{
    internal interface ISchedulerService
    {
        void Start(Assembly assembly);
    }
}