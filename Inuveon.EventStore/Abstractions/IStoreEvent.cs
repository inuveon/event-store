namespace Inuveon.EventStore.Abstractions;

public interface IStoreEvent
{
    /// <summary>
    /// Gets the unique identifier of the event record.
    /// </summary>
    Guid Id { get; init; }

    /// <summary>
    /// Gets the identifier for the event stream, which is the AggregateId.
    /// </summary>
    Guid StreamId { get; init; }

    /// <summary>
    /// Gets the type of the event.
    /// </summary>
    string EventType { get; init; }

    /// <summary>
    /// Gets the actual domain event data.
    /// </summary>
    IDomainEvent Data { get; init; }

    /// <summary>
    /// Gets the timestamp of when the event was recorded.
    /// </summary>
    DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// Gets the version of the event within its stream.
    /// </summary>
    long Version { get; init; }

    bool Equals(StoreEvent? other);
    bool Equals(object? other);
    int GetHashCode();
    string ToString();
}