using System.Diagnostics;
using Inuveon.EventStore.Abstractions.Entities;
using Inuveon.EventStore.Abstractions.Storage;

namespace Inuveon.EventStore;

internal class EventStore(IEventStoreProvider provider) : IEventStore
{
    public async Task AppendEventsAsync(IAggregateRoot aggregate, CancellationToken cancellationToken)
    {
        await provider.AppendEventsAsync(aggregate, cancellationToken);
    }

    public async Task<IAggregateRoot> LoadAggregateAsync<TAggregate>(Guid aggregateId,
        CancellationToken cancellationToken) where TAggregate : IAggregateRoot, new()
    {
        return await provider.LoadAggregateAsync<TAggregate>(aggregateId, cancellationToken);
    }
}