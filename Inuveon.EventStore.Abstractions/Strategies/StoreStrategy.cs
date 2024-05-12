namespace Inuveon.EventStore.Abstractions.Strategies;

public enum StoreStrategy
{
    OneStreamPerAggregate, // Default strategy
    SingleStream
}
