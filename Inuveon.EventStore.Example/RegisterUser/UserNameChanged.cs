using Inuveon.EventStore.Abstractions;
using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Example.RegisterUser;

public record UserNameChanged(
    Guid MessageId,
    Guid AggregateId,
    string FirstName,
    DateTimeOffset Timestamp,
    long Version,
    Guid? CorrelationId, Guid? CausationId) : IDomainEvent;