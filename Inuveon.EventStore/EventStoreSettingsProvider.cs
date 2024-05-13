using Inuveon.EventStore.Abstractions.Storage;

namespace Inuveon.EventStore;

public record EventStoreSettingsProvider(IEnumerable<EventStream> EventStreams, IEventStoreOptions EventStoreOptions)
    : IEventStoreSettingsProvider;