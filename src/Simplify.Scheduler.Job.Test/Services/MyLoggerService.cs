using Microsoft.Extensions.Logging;
using Simplify.Scheduler.Job.Test.Interfaces;

namespace Simplify.Scheduler.Job.Test.Services;

public class MyLoggerService : IMyLoggerService
{
    private readonly ILogger<MyLoggerService> _logger;

    public MyLoggerService(ILogger<MyLoggerService> logger)
        => _logger = logger;

    public async Task ExecuteJobAsync()
    {
        _logger.LogInformation("Execute job at: {0}", DateTime.Now);
        await Task.Delay(5000);
    }
}