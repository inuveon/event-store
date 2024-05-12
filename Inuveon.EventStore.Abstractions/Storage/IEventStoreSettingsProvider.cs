namespace Inuveon.EventStore.Abstractions.Storage;

public interface IEventStoreSettingsProvider
{
    string ConnectionString { get; } 
    string DatabaseName { get; }
    string ApplicationName { get; } 
    int Throughput { get; }
    IEnumerable<EventStream> EventStreams { get; }
}