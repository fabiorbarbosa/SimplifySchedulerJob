using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Simplify.Scheduler.Job.Test.Helpers;

public class InjectionFixture
{
    private readonly WebApplicationBuilder server;
    public readonly IConfiguration Configuration;

    public InjectionFixture()
    {
        server = WebApplication.CreateBuilder([]);
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
        Configuration = builder.Build();
    }

    public IServiceCollection ServiceCollection => server.Services;

    public WebApplication BuildApplication()
        => server.Build();
}
