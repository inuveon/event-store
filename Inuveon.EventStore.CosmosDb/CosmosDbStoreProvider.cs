using Inuveon.EventStore.Abstractions;

namespace Inuveon.EventStore.CosmosDb;

public class CosmosDbStoreProvider : IEventStoreProvider
{
    public Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IAggregateRoot> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken) where TAggregate : IAggregateRoot, new()
    {
        throw new NotImplementedException();
    }
}