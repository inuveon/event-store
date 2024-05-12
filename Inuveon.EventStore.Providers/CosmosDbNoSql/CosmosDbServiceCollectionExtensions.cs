using System.Text.Json;
using Inuveon.EventStore.Abstractions.Converters;
using Inuveon.EventStore.Abstractions.Storage;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace Inuveon.EventStore.Providers.CosmosDbNoSql;

public static class CosmosDbServiceCollectionExtensions
{
    public static IServiceCollection AddCosmosDbEventStoreProvider(this IServiceCollection services, IEventStoreSettingsProvider settings)
    {
        CosmosClientOptions clientOptions = new CosmosClientOptions
        {
            Serializer = new CustomCosmosSerializer(),
            ApplicationName = settings.ApplicationName
        };
        
        var client = new CosmosClient(settings.ConnectionString, clientOptions);
        services.AddSingleton(client);

        services.AddSingleton<IEventStoreProviderInitializer, CosmosDbEventStoreInitializer>();
        services.AddSingleton(client.GetDatabase(settings.DatabaseName));
        services.AddSingleton<IEventStoreProvider, CosmosDbStoreProvider>();
        
        return services;
    }
}

