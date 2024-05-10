namespace Inuveon.EventStore.Abstractions
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