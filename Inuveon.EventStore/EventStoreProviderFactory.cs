using System.Diagnostics;
using Inuveon.EventStore.Abstractions.Storage;
using Inuveon.EventStore.Providers;
using Inuveon.EventStore.Providers.CosmosDbNoSql;
using Microsoft.Extensions.DependencyInjection;

namespace Inuveon.EventStore;

public static class EventStoreProviderFactory
{
    public static IServiceCollection RegisterProvider(this IServiceCollection services, EventStoreOptions options)
    {
        var providerSettings = services.BuildServiceProvider().GetService<IEventStoreSettingsProvider>();
        if (providerSettings == null) throw new InvalidOperationException("No provider settings found");

        switch (options.StoreProvider)
        {
            case "CosmosDB":
                services.AddCosmosDbEventStoreProvider(providerSettings);
                Debug.WriteLine("CosmosDB provider registered");
                break;
            case "DynamoDB":
                // Assuming there's a way to create a DynamoDB client
                //return new DynamoDbEventStoreInitializer(/* dependencies */);
                Debug.WriteLine("DynamoDb provider registered");
                break;
            default:
                throw new ArgumentException($"Unsupported store provider: {options.StoreProvider}");
        }


        services.AddHostedService<EventStoreInitializerService>();
        return services;
    }
}