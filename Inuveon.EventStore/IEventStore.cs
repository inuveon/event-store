using Inuveon.EventStore.Abstractions.Entities;

namespace Inuveon.EventStore;

public interface IEventStore
{
    Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);

    Task<IAggregateRoot> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new();
}