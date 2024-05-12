using System.Reflection;
using Inuveon.EventStore.Extensions;
using Inuveon.EventStore.Providers;
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
            options.StoreProvider = eventStoreConfig["Provider"] ?? "CosmosDB";
            options.AssemblyFilter = assembly => Assembly.GetExecutingAssembly().FullName!.Contains("Inuveon.EventStore");

            options.AssembliesToScan = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => options.AssemblyFilter(assembly))
                .ToArray();
        });
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    });

// Build and start the host
var host = builder.Build();

// Resolve the service you need and call a method, for example:
// var myService = host.Services.GetService<IMyService>();
// myService.DoSomething();

var env = host.Services.GetRequiredService<IHostEnvironment>();
Console.WriteLine($"Current environment: {env.EnvironmentName}");

await host.RunAsync();