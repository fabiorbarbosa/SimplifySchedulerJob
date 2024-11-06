using System;

namespace Simplify.Scheduler.Job.Attributes
{
    /// <summary>
    /// Attribute to configure the cron expression.
    /// </summary>
    /// <remarks>
    /// Use cron expression to execute the 'IJobService.ExecuteJobAsync' method.
    /// </remarks>
    /// <param name="cronJobSchedule">Cron Expression</param>
    /// <author>Fábio Barbosa</author>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class JobServiceAttribute : Attribute
    {
        internal readonly string CronJobSchedule;

        /// <summary>
        /// Attribute to configure the cron expression.
        /// </summary>
        /// <remarks>
        /// Use cron expression to execute the 'IJobService.ExecuteJobAsync' method.
        /// </remarks>
        /// <param name="cronJobSchedule">Cron Expression</param>
        public JobServiceAttribute(string cronJobSchedule)
            => CronJobSchedule = cronJobSchedule;
    }
}