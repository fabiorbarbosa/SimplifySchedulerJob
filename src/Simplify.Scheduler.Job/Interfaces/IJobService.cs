using System.Threading.Tasks;

namespace Simplify.Scheduler.Job.Interfaces
{
    /// <summary>
    /// Implementation interface for Simplify.Scheduler.Job.
    /// </summary>
    /// <remarks>
    /// Need to inherit in your service interface.
    /// </remarks>
    /// <author>Fábio Barbosa</author>
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