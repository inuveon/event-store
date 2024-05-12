using System.Text.Json;
using Inuveon.EventStore.Abstractions.Converters;
using Inuveon.EventStore.Abstractions.Storage;
using Inuveon.EventStore.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Inuveon.EventStore.Extensions;

public static class EventStoreServiceCollectionExtensions
{
    public static IServiceCollection AddEventStore(this IServiceCollection services, Action<EventStoreOptions> configureOptions)
    {
        var options = new EventStoreOptions();
        configureOptions(options);

        // Assuming EventStoreConfigurator is a utility to register streams
        var assembliesToScan = options.AssembliesToScan ?? AppDomain.CurrentDomain.GetAssemblies();
        var registeredStreams = EventStoreConfigurator.RegisterAggregateStreams(assembliesToScan);
        var registeredStreamsProvider = new EventStoreSettingsProvider(registeredStreams, "Your-ConnectionString", "CosmosDB", "InuveonEventStore", 400);
        
        services.AddSingleton<IEventStoreSettingsProvider>(registeredStreamsProvider);
        services.AddSingleton(new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new DomainEventJsonConverter() }
        });
        
        // Instantiate and register the initializer
        services.RegisterProvider(options);
        services.AddHostedService<EventStoreInitializerService>();

        
        // Register the EventStore with configured options
        services.AddSingleton<IEventStore, EventStore>();

        return services;
    }
}