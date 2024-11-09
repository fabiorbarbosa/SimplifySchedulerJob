using Simplify.Scheduler.Job.Test.Models;

namespace Simplify.Scheduler.Job.Test.Interfaces;

[JobService(typeof(MyFirstConfig))]
public interface IMyFirstService : IJobService
{

}