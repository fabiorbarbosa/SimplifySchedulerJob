using Simplify.Scheduler.Job.Tests.Models;

namespace Simplify.Scheduler.Job.Tests.Interfaces;

[JobService(typeof(MyFirstConfig))]
public interface IMyFirstService : IJobService
{

}