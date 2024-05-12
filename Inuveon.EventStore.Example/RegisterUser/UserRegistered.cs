using Inuveon.EventStore.Abstractions;
using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Example.RegisterUser;

/// <summary>
/// A test event implementation.
/// </summary>
public record UserRegistered(
    Guid MessageId,
    Guid AggregateId,
    string FirstName,
    string EmailAddress,
    DateTimeOffset Timestamp,
    long Version,
    Guid? CorrelationId, Guid? CausationId) : IDomainEvent;