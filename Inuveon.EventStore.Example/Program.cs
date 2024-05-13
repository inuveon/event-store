using System.Diagnostics;
using System.Reflection;
using Inuveon.EventStore.Abstractions.Strategies;
using Inuveon.EventStore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var eventStoreConfig = hostContext.Configuration.GetSection("EventStore");

        // Configure EventStore
        services.AddEventStore(options =>
        {
            options.ConnectionString = eventStoreConfig["ConnectionString"]!;
            options.DatabaseName = eventStoreConfig["DatabaseName"]!;
            options.ApplicationName = eventStoreConfig["ApplicationName"]!;
            options.StoreStrategy = StoreStrategy.OneStreamPerAggregate;
            options.Throughput = 400;
            options.StoreProvider = eventStoreConfig["Provider"] ?? "CosmosDB";
            options.AssemblyFilter =
                assembly => Assembly.GetExecutingAssembly().FullName!.Contains("Inuveon.EventStore");

            options.AssembliesToScan = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => options.AssemblyFilter(assembly))
                .ToArray();
        });
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", false, true);
        config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
    });

// Build and start the host
var host = builder.Build();

// Resolve the service you need and call a method, for example:
// var myService = host.Services.GetService<IMyService>();
// myService.DoSomething();

var env = host.Services.GetRequiredService<IHostEnvironment>();
Console.WriteLine($"Current environment: {env.EnvironmentName}");

await host.RunAsync();