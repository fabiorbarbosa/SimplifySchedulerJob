
# SimplifySchedulerJob

Component to facilitate the use of Quartz.NET, and eliminating the complex JobFactory configuration.


## Compatibility

SimplifySchedulerJob supports .NET Core/netstandard 2.0.
## Dependencies

* Microsoft.AspNetCore.Http.Abstractions 2.2.0
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
In the service interface, inherit the `IJobService` interface and configure the `JobService` attribute with the structure configured in `appSettings.json`.
```csharp
[JobService("MyJobService:Cron")]
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
services.AddSimplifySchedulerJob(assembly);
...
```

Start SchedulerJob before starting the application.
```csharp
...
app.UseSimplifySchedulerJob(assembly);
app.Run();
...

```

## License

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License [here](https://choosealicense.com/licenses/mit/).


