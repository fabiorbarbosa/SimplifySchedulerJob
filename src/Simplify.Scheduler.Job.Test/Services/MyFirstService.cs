
using Microsoft.Extensions.Logging;
using Simplify.Scheduler.Job.Test.Interfaces;

namespace Simplify.Scheduler.Job.Test.Services;

public class MyFirstService : IMyFirstService
{
    private readonly ILogger<MyFirstService> _logger;

    public MyFirstService(ILogger<MyFirstService> logger)
        => _logger = logger;

    public async Task ExecuteJobAsync()
    {
        _logger.LogInformation("Execute job at: {0}", DateTime.Now);
        await Task.Delay(5000);
    }
}