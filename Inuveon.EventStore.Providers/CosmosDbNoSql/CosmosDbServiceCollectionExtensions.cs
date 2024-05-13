using Inuveon.EventStore.Abstractions.Storage;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace Inuveon.EventStore.Providers.CosmosDbNoSql;

public static class CosmosDbServiceCollectionExtensions
{
    public static IServiceCollection AddCosmosDbEventStoreProvider(this IServiceCollection services,
        IEventStoreSettingsProvider settings)
    {
        var clientOptions = new CosmosClientOptions
        {
            Serializer = new CustomCosmosSerializer(),
            ApplicationName = settings.EventStoreOptions.ApplicationName
        };

        var client = new CosmosClient(settings.EventStoreOptions.ConnectionString, clientOptions);
        services.AddSingleton(client);

        services.AddSingleton<IEventStoreProviderInitializer, CosmosDbEventStoreInitializer>();
        services.AddSingleton(client.GetDatabase(settings.EventStoreOptions.DatabaseName));
        services.AddSingleton<IEventStoreProvider, CosmosDbStoreProvider>();

        return services;
    }
}