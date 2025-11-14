using Simplify.Scheduler.Job.Tests.Models;

namespace Simplify.Scheduler.Job.Tests.Interfaces;

[JobService(typeof(MySecondConfig))]
public interface IMySecondService : IJobService
{

}