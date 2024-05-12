
using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Example.RegisterUser;

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