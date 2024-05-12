using System.Text.Json.Serialization;
using Inuveon.EventStore.Abstractions.Entities;
using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Abstractions.Storage
{
    /// <summary>
    /// Represents a stored event in the event store.
    /// </summary>
    public sealed record StoreEvent() 
    {
        /// <summary>
        /// Gets the unique identifier of the event record.
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the identifier for the event stream, which is the AggregateId.
        /// </summary>
        [JsonPropertyName("streamId")]
        public Guid StreamId { get; init; }

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        [JsonPropertyName("eventType")]
        public string EventType { get; init; } = null!;

        /// <summary>
        /// Gets the actual domain event data.
        /// </summary>
        [JsonPropertyName("data")]
        public IDomainEvent Data { get; init; } = null!;

        /// <summary>
        /// Gets the timestamp of when the event was recorded.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// Gets the version of the event within its stream.
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreEvent"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the event record.</param>
        /// <param name="streamId">The identifier for the event stream.</param>
        /// <param name="eventType">The type of the event.</param>
        /// <param name="data">The actual domain event data.</param>
        /// <param name="timestamp">The timestamp of when the event was recorded.</param>
        /// <param name="version">The version of the event within its stream.</param>
        private StoreEvent(Guid id, Guid streamId, string eventType, IDomainEvent data, DateTimeOffset timestamp, long version) : this()
        {
            Id = id;
            StreamId = streamId;
            EventType = eventType;
            Data = data;
            Timestamp = timestamp;
            Version = version;
        }

        /// <summary>
        /// Creates a new <see cref="StoreEvent"/> instance.
        /// </summary>
        /// <param name="aggregate">The aggregate root associated with the event.</param>
        /// <param name="event">The domain event to be stored.</param>
        /// <returns>A new <see cref="StoreEvent"/> instance.</returns>
        public static StoreEvent Create(IAggregateRoot aggregate, IDomainEvent @event)
            => new(@event.MessageId, aggregate.Id, @event.GetType().FullName!, @event, @event.Timestamp, @event.Version);
    }
}