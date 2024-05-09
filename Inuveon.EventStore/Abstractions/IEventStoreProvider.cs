namespace Inuveon.EventStore.Abstractions;

public interface IEventStoreProvider
{
    Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken);

    Task<IAggregateRoot> LoadAggregateAsync<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot, new();
}