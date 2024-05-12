using Inuveon.EventStore.Abstractions.Entities;
using Inuveon.EventStore.Abstractions.Storage;

namespace Inuveon.EventStore.Providers.DynamoDb;

public class DynamoDbEventStoreInitializer : IEventStoreProvider
{
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IAggregateRoot> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken) where TAggregate : IAggregateRoot, new()
    {
        throw new NotImplementedException();
    }
}