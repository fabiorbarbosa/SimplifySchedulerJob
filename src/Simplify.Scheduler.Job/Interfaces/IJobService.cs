using System.Threading.Tasks;

namespace Simplify.Scheduler.Job
{
    /// <summary>
    /// Implementation interface for Simplify.Scheduler.Job.
    /// </summary>
    /// <remarks>
    /// Need to inherit in your service interface.
    /// </remarks>
    public interface IJobService
    {
        /// <summary>
        /// Method executed by Simplify.Scheduler.Job.
        /// </summary>
        /// <remarks>
        /// Implement your execution routine in this method.
        /// </remarks>
        Task ExecuteJobAsync();
    }
}