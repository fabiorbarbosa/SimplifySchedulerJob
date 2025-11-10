
# SimplifySchedulerJob

Component to facilitate the use of Quartz.NET, and eliminating the complex JobFactory configuration.

## Compatibility

SimplifySchedulerJob supports .NET 6.0/7.0/8.0 and .NET Standard 2.0.

## Dependencies

* Microsoft.Extensions.Configuration.Binder 2.2.0
* Microsoft.Extensions.DependencyInjection.Abstractions 8.0.2
* Quartz.AspNetCore 3.13.1

## Installation

With .NET CLI

```bash
  dotnet add package SimplifySchedulerJob
```

Or with Nuget CLI

```bash
  NuGet\Install-Package SimplifySchedulerJob
```

## Usage/Examples

Add the configuration to your project's `appSettings.json` file.

```json
{
  "MyJobService": {
    "Cron": "0 0/1 * * * ?"
  }
}
```

Create a class that represents the configuration of the `appSettings.json` and inherit the `JobOptions` class.

```csharp
public class MyLogger : JobOptions
{

}
```

In the service interface, inherit the `IJobService` interface and pass the type of the class that represents the configuration of the `appSettings.json`.

```csharp
[JobService(typeof(MyLogger))]
public interface IMyJobService : IJobService
{

}
```

In the service, implement the `ExecuteJobAsync` method with the routine you want to execute.

```csharp

public class MyJobService : IMyJobService
{
    ...
    public async Task ExecuteJobAsync()
    {
        // YOUR CODE
    }
    ...
}
```

In dependency injection, register the created service and add the `SimplifySchedulerJob` dependencies.

```csharp
...
// REGISTER YOUR SERVICE
services.AddTransient(typeof(IMyJobService), typeof(MyJobService));

// REGISTER SIMPLIFYSCHEDULERJOB DEPENDENCIES
services.AddSimplifySchedulerJob(builder.Configuration, assembly);
...
```

Start SimplifySchedulerJob before starting the application.

```csharp
...
app.UseSimplifySchedulerJob(assembly);
app.Run();
...

```

## License

See [LICENSE](https://raw.githubusercontent.com/fabiorbarbosa/SimplifySchedulerJob/refs/heads/main/LICENSE) for details.
