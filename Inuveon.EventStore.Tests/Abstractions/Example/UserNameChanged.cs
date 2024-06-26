using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Tests.Abstractions.Example;

public record UserNameChanged(
    Guid MessageId,
    Guid AggregateId,
    string FirstName,
    DateTimeOffset Timestamp,
    long Version,
    Guid? CorrelationId,
    Guid? CausationId) : IDomainEvent;