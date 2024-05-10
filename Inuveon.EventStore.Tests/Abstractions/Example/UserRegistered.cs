using Inuveon.EventStore.Abstractions;

namespace Inuveon.EventStore.Tests.Abstractions.Example;

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