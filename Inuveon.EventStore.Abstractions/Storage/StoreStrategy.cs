namespace Inuveon.EventStore.Abstractions.Storage;

public enum StoreStrategy
{
    OneStreamPerAggregate, // Default strategy
    SingleStream
}
