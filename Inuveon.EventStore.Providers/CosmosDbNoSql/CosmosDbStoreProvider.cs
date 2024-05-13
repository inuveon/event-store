using System.Diagnostics;
using System.Text.Json;
using Inuveon.EventStore.Abstractions.Entities;
using Inuveon.EventStore.Abstractions.Exceptions;
using Inuveon.EventStore.Abstractions.Storage;
using Microsoft.Azure.Cosmos;

namespace Inuveon.EventStore.Providers.CosmosDbNoSql;

public class CosmosDbStoreProvider(Database database, JsonSerializerOptions jsonSerializerSettings)
    : IEventStoreProvider
{
    public async Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
    {
        var containerName = aggregate.GetType().Name.ToLower();
        var container = database.GetContainer(containerName);
        var partitionKey = new PartitionKey(aggregate.Id.ToString());

        var transactionalBatch = container.CreateTransactionalBatch(partitionKey);

        foreach (var @event in aggregate.UncommittedEvents)
        {
            var storeEvent = StoreEvent.Create(aggregate, @event);
            transactionalBatch = transactionalBatch.CreateItem(storeEvent);
        }

        var response = await transactionalBatch.ExecuteAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
            // Log the error or handle it as needed
            throw new Exception($"Failed to commit batch: {response.ErrorMessage}");
    }

    public async Task<IAggregateRoot> LoadAggregateAsync<TAggregate>(Guid aggregateId,
        CancellationToken cancellationToken) where TAggregate : IAggregateRoot, new()
    {
        var query = new QueryDefinition(
                "select * from t where t.streamId = @aggregateId order by t.version asc")
            .WithParameter("@aggregateId", aggregateId.ToString());

        var container = database.GetContainer(typeof(TAggregate).Name.ToLower());
        var partitionKey = new PartitionKey(aggregateId.ToString());
        var iterator = container.GetItemQueryIterator<dynamic>(query,
            requestOptions: new QueryRequestOptions { PartitionKey = partitionKey });

        var events = new List<StoreEvent>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync(cancellationToken);
            foreach (var doc in response)
            {
                // Use custom JsonSerializerSettings here
                string json = JsonSerializer.Serialize(doc);
                var storeEvent = JsonSerializer.Deserialize<StoreEvent>(json, jsonSerializerSettings);
                events.Add(storeEvent!);

                Debug.WriteLine(
                    $"Loaded Event - ID: {storeEvent!.Id}, Type: {storeEvent.EventType}, Version: {storeEvent.Version}");
            }
        }

        if (!events.Any())
            throw new AggregateNotFoundException(aggregateId, typeof(TAggregate), "No events found for the aggregate.");

        Debug.WriteLine($"Total events loaded for Aggregate ID {aggregateId}: {events.Count}");

        var aggregate = new TAggregate();
        aggregate.LoadFromHistory(events.Select(e => e.Data).ToList());

        return aggregate;
    }
}