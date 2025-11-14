using Microsoft.Extensions.Logging;
using Simplify.Scheduler.Job.Tests.Interfaces;

namespace Simplify.Scheduler.Job.Tests.Services;

public class MySecondService : IMySecondService
{
    private readonly ILogger<MySecondService> _logger;

    public MySecondService(ILogger<MySecondService> logger)
        => _logger = logger;

    public async Task ExecuteJobAsync()
    {
        _logger.LogInformation("Execute job at: {0}", DateTime.Now);
        await Task.Delay(10000);
    }
}