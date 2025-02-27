namespace Simplify.Scheduler.Job;

public abstract class JobOptions
{
    public string[] CronExpressions { get; set; }
    public string TimeZoneId { get; set; }
}
