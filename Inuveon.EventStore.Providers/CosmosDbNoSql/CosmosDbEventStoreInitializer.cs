using Inuveon.EventStore.Abstractions.Storage;
using Microsoft.Azure.Cosmos;

namespace Inuveon.EventStore.Providers.CosmosDbNoSql;

public class CosmosDbEventStoreInitializer(CosmosClient client, IEventStoreSettingsProvider settings)
    : IEventStoreProviderInitializer
{
    public async Task InitializeAsync()
    {
        var databaseResponse = await client.CreateDatabaseIfNotExistsAsync(settings.EventStoreOptions.DatabaseName,
            settings.EventStoreOptions.Throughput);
        foreach (var stream in settings.EventStreams)
            await CreateContainerAsync(databaseResponse.Database, stream.StreamName, stream.PartitionKeyPath);
    }

    private async Task CreateContainerAsync(Database database, string containerName, string partitionKeyPath)
    {
        var properties = new ContainerProperties(containerName, partitionKeyPath);
        await database.CreateContainerIfNotExistsAsync(properties);
    }
}