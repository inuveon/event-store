using Inuveon.EventStore.Abstractions.Storage;

namespace Inuveon.EventStore;

public record EventStoreSettingsProvider(IEnumerable<EventStream> EventStreams, string ConnectionString, string DatabaseName, string ApplicationName, int Throughput = 400 ) : IEventStoreSettingsProvider;