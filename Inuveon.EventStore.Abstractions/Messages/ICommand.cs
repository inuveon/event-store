namespace Inuveon.EventStore.Abstractions.Messages;

/// <summary>
///     Represents a command that directs a system to perform a specific action.
///     Extends IMessage to include necessary identifiers and timestamp information.
/// </summary>
public interface ICommand : IMessage
{
}