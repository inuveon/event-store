using System.Text.Json;
using Inuveon.EventStore.Abstractions.Converters;
using Inuveon.EventStore.Abstractions.Storage;
using Inuveon.EventStore.Providers.CosmosDbNoSql;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace Inuveon.EventStore.Providers;

public static class RegisterProvider
{
    // public static IServiceCollection AddEventStore(this IServiceCollection services, EventStoreOptions options)
    // {
    //     CosmosClientOptions clientOptions = new CosmosClientOptions
    //     {
    //         Serializer = new CustomCosmosSerializer(),
    //         ApplicationName = options.ApplicationName
    //     };
    //     
    //     var client = new CosmosClient(options.ConnectionString, clientOptions);
    //     services.AddSingleton(client);
    //
    //     var initializer = new CosmosDbEventStoreInitializer(client, options);
    //     services.AddSingleton(initializer);
    //     services.AddHostedService<EventStoreInitializer>();
    //
    //     services.AddSingleton(client.GetDatabase(options.DatabaseName));
    //     services.AddSingleton<IEventStoreProvider, CosmosDbStoreProvider>();
    //     
    //     services.AddSingleton(new JsonSerializerOptions
    //     {
    //         WriteIndented = true,
    //         Converters = { new DomainEventJsonConverter() }
    //     });
    //     
    //     return services;
    // }
}