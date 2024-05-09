namespace Inuveon.EventStore.Exceptions;

public class AggregateNotFoundException(Guid aggregateId, Type type)
    : Exception($"Aggregate {type.Name} with id {aggregateId} not found");