namespace Inuveon.EventStore.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an aggregate root is not found.
    /// </summary>
    public class AggregateNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateNotFoundException"/> class.
        /// </summary>
        /// <param name="aggregateId">The unique identifier of the aggregate root that was not found.</param>
        /// <param name="type">The type of the aggregate root that was not found.</param>
        public AggregateNotFoundException(Guid aggregateId, Type type)
            : base($"Aggregate {type.Name} with id {aggregateId} not found")
        {
        }
    }
}