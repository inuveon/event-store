namespace Inuveon.EventStore.Abstractions
{
    /// <summary>
    /// Represents a domain event that is a specific type of message.
    /// </summary>
    /// <remarks>
    /// A domain event is something that has happened in the past, 
    /// which is of interest to the business and can trigger behavior in the system.
    /// </remarks>
    public interface IDomainEvent : IMessage
    {
        /// <summary>
        /// Gets the unique identifier of the aggregate root entity to which this event belongs.
        /// </summary>
        /// <value>
        /// The unique identifier of the aggregate root entity, represented as a Guid.
        /// </value>
        Guid AggregateId { get; }

        /// <summary>
        /// Gets the version of the event.
        /// </summary>
        /// <value>
        /// The version of the event, represented as a long.
        /// This can be used to ensure events are applied in the correct order.
        /// </value>
        long Version { get; }
        
        /// <summary>
        /// Gets an optional causation identifier for the event.
        /// </summary>
        /// <value>
        /// The causation identifier that indicates which event or command caused this event to be raised, if applicable.
        /// </value>
        Guid? CausationId { get; }
    }
}
