using Inuveon.EventStore.Abstractions.Storage;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Hosting;

namespace Inuveon.EventStore.Providers.CosmosDbNoSql;

public class CosmosDbEventStoreInitializer(CosmosClient client, IEventStoreSettingsProvider settings) : IEventStoreProviderInitializer
{
    public async Task InitializeAsync()
    {
        DatabaseResponse databaseResponse = await client.CreateDatabaseIfNotExistsAsync(settings.DatabaseName, settings.Throughput);
        foreach (var stream in settings.EventStreams)
        {
            await CreateContainerAsync(databaseResponse.Database, stream.StreamName, stream.PartitionKeyPath);
        }
    }

    private async Task CreateContainerAsync(Database database, string containerName, string partitionKeyPath)
    {
        ContainerProperties properties = new ContainerProperties(containerName, partitionKeyPath);
        await database.CreateContainerIfNotExistsAsync(properties);
    }
}