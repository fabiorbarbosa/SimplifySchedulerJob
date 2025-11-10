using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Simplify.Scheduler.Job.Test.Helpers;
using Simplify.Scheduler.Job.Test.Interfaces;
using Simplify.Scheduler.Job.Test.Models;
using Simplify.Scheduler.Job.Test.Services;

namespace Simplify.Scheduler.Job.Test;

public class SchedulerServiceTest(InjectionFixture injection)
    : IClassFixture<InjectionFixture>
{
    [Fact]
    public void AddSimplifySchedulerJob_ConfiguresSchedulerAndBindsCronOptions()
    {
        var assembly = typeof(SchedulerServiceTest).Assembly;
        injection.ServiceCollection.AddTransient(typeof(IMyFirstService), typeof(MyFirstService));
        injection.ServiceCollection.AddTransient(typeof(IMySecondService), typeof(MySecondService));
        injection.ServiceCollection.AddSimplifySchedulerJob(injection.Configuration, assembly);
        var app = injection.BuildApplication();
        app.UseSimplifySchedulerJob(assembly);
        using var scope = app.Services.CreateScope();
        var provider = scope.ServiceProvider;

        var schedulerServiceType = typeof(IJobService).Assembly
            .GetType("Simplify.Scheduler.Job.Interfaces.ISchedulerService");
        Assert.NotNull(schedulerServiceType);

        var schedulerService = provider.GetService(schedulerServiceType!);
        Assert.NotNull(schedulerService);

        var firstOptions = provider.GetRequiredService<IOptions<MyFirstConfig>>();
        Assert.Equal("0 0/1 * * * ?", firstOptions.Value.CronExpression);

        var secondOptions = provider.GetRequiredService<IOptions<MySecondConfig>>();
        Assert.Equal("0 0/2 * * * ?", secondOptions.Value.CronExpression);
    }
}
