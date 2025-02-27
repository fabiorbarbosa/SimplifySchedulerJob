using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Simplify.Scheduler.Job.Test.Helpers;
using Simplify.Scheduler.Job.Test.Interfaces;
using Simplify.Scheduler.Job.Test.Services;
using Microsoft.AspNetCore.Builder;

namespace Simplify.Scheduler.Job.Test;

public class SchedulerServiceTest : IClassFixture<InjectionFixture>
{
    private readonly InjectionFixture _injection;

    public SchedulerServiceTest(InjectionFixture injection)
        => _injection = injection;

    [Fact]
    public void Test1()
    {
        _injection.ServiceCollection.AddTransient<IMyFirstService, MyFirstService>();
        _injection.ServiceCollection.AddTransient<IMySecondService, MySecondService>();
        _injection.ServiceCollection.AddSimplifySchedulerJob(_injection.Configuration);
        WebApplication app = _injection.BuildApplication();
        app.UseSimplifySchedulerJob();
        app.Run();
        Assert.Empty(string.Empty);
    }
}