namespace Inuveon.EventStore.Abstractions.Exceptions
{
    public class AggregateNotFoundException(Guid aggregateId, Type aggregateType, string message) : Exception(message)
    {
        public Guid AggregateId { get; } = aggregateId;
        public Type AggregateType { get; } = aggregateType;

        // Constructor for the exception, accepts the aggregate ID, the type of the aggregate, and a custom message
        // Pass the custom message to the base Exception class

        // Optionally, override the ToString() method to provide detailed information about the exception
        public override string ToString()
        {
            return $"AggregateNotFoundException: {Message} - Aggregate ID: {AggregateId}, Aggregate Type: {AggregateType.Name}";
        }
    }

}