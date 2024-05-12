using Inuveon.EventStore.Abstractions.Entities;

namespace Inuveon.EventStore.Abstractions.Storage;

public interface IEventStoreProvider
{
    Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);

    Task<IAggregateRoot> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new();
}