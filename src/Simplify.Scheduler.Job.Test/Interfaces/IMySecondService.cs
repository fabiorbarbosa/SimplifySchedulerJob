using Simplify.Scheduler.Job.Test.Models;

namespace Simplify.Scheduler.Job.Test.Interfaces;

[JobService(typeof(MySecondConfig))]
public interface IMySecondService : IJobService
{

}