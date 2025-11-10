namespace Simplify.Scheduler.Job
{
    /// <summary>
    /// Base contract for configuration sections consumed by a job.
    /// </summary>
    /// <remarks>
    /// Create a derived class per job interface and expose strongly typed properties that map to the corresponding configuration section.
    /// </remarks>
    public abstract class JobOptions
    {
        /// <summary>
        /// Cron expression that defines the trigger frequency for the job.
        /// </summary>
        public string CronExpression { get; set; }
    }
}
