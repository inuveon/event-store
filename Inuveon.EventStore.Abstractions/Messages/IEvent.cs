namespace Inuveon.EventStore.Abstractions.Messages
{
    /// <summary>
    /// Represents an event that is a specific type of message.
    /// </summary>
    /// <remarks>
    /// An event is a change in the state of the system that can trigger behavior.
    /// </remarks>
    public interface IEvent : IMessage
    {
    }
}