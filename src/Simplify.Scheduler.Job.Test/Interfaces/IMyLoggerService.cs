using Simplify.Scheduler.Job.Test.Models;

namespace Simplify.Scheduler.Job.Test.Interfaces;

[JobService(typeof(MyLogger))]
public interface IMyLoggerService : IJobService
{

}