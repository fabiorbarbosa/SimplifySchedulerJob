using System;

namespace Simplify.Scheduler.Job
{
    /// <summary>
    /// Declares configuration metadata for a Simplify Scheduler job interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    public class JobServiceAttribute : Attribute
    {
        internal readonly Type TypeOptions;

        /// <summary>
        /// Tags a job interface with the options type that supplies its configuration.
        /// </summary>
        /// <remarks>
        /// The options type must inherit <see cref="JobOptions"/> and will be bound to configuration sections bearing the same name.
        /// </remarks>
        /// <param name="typeOptions">Concrete <see cref="JobOptions"/> type that holds the cron expression and any extra parameters.</param>
        public JobServiceAttribute(Type typeOptions)
            => TypeOptions = typeOptions;
    }
}
