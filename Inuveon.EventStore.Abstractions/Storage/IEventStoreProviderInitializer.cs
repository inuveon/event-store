namespace Inuveon.EventStore.Abstractions.Storage;

public interface IEventStoreProviderInitializer
{
    Task InitializeAsync();
}