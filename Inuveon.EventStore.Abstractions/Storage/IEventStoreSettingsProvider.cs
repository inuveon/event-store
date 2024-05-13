namespace Inuveon.EventStore.Abstractions.Storage;

public interface IEventStoreSettingsProvider
{
    IEventStoreOptions EventStoreOptions { get; }
    IEnumerable<EventStream> EventStreams { get; }
}