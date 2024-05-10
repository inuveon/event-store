using Inuveon.EventStore.Abstractions;

namespace Inuveon.EventStore.Tests.Abstractions.Example;

/// <summary>
/// The command to register a user.
/// </summary>
public record RegisterUserCommand(
    Guid MessageId,
    string FirstName,
    string EmailAddress,
    DateTimeOffset Timestamp,
    long Version,
    Guid? CorrelationId) : ICommand;