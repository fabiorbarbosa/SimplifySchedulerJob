using System;

namespace Simplify.Scheduler.Job
{
    /// <summary>
    /// Attribute to configure the cron expression.
    /// </summary>
    /// <remarks>
    /// Use cron expression to execute the 'IJobService.ExecuteJobAsync' method.
    /// </remarks>
    /// <param name="cronJobSchedule">Cron Expression</param>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class JobServiceAttribute : Attribute
    {
        internal readonly Type TypeOptions;

        /// <summary>
        /// Attribute to configure the cron expression.
        /// </summary>
        /// <remarks>
        /// Use cron expression to execute the 'IJobService.ExecuteJobAsync' method.
        /// </remarks>
        /// <param name="typeOptions"></param>
        public JobServiceAttribute(Type typeOptions)
            => TypeOptions = typeOptions;
    }
}