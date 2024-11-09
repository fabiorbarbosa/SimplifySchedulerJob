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
        var assembly = Assembly.GetAssembly(typeof(SchedulerServiceTest));
        _injection.ServiceCollection.AddTransient(typeof(IMyFirstService), typeof(MyFirstService));
        _injection.ServiceCollection.AddTransient(typeof(IMySecondService), typeof(MySecondService));
        _injection.ServiceCollection.AddSimplifySchedulerJob(_injection.Configuration, assembly);
        var app = _injection.BuildApplication();
        app.UseSimplifySchedulerJob(assembly);
        app.Run();
        Assert.Empty(string.Empty);
    }
}