using System.Text.Json.Serialization;

namespace Inuveon.EventStore.Abstractions;

public sealed record StoreEvent()
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; } // Unique identifier for the event record

    [JsonPropertyName("streamId")]
    public Guid StreamId { get; init; } // Identifier for the event stream, AggregateId!

    [JsonPropertyName("eventType")]
    public string EventType { get; init; } // Type of the event

    [JsonPropertyName("data")]
    public IDomainEvent Data { get; init; } // The actual domain event data

    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; init; } // Timestamp of when the event was recorded

    [JsonPropertyName("version")]
    public long Version { get; init; } // Version of the event within its stream
    
    private StoreEvent(Guid id, Guid streamId, string eventType, IDomainEvent data, DateTimeOffset timestamp, long version) : this()
    {
        Id = id;
        StreamId = streamId;
        EventType = eventType;
        Data = data;
        Timestamp = timestamp;
        Version = version;
    }
    
    public static StoreEvent Create(IAggregateRoot aggregate, IDomainEvent @event)
        => new(@event.Id, aggregate.Id, @event.GetType().FullName, @event, @event.Timestamp, @event.Version);
}