using Simplify.Scheduler.Job.Attributes;
using Simplify.Scheduler.Job.Interfaces;

namespace Simplify.Scheduler.Job.Test.Interfaces;

[JobService("MyLoggerService:Cron")]
public interface IMyLoggerService : IJobService
{

}