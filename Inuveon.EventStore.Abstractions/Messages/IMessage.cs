namespace Inuveon.EventStore.Abstractions.Messages;

/// <summary>
///     Represents a message with a unique identifier, a correlation identifier, and a timestamp.
/// </summary>
public interface IMessage
{
    /// <summary>
    ///     Gets the unique identifier of the message.
    /// </summary>
    /// <value>
    ///     The unique identifier of the message, represented as a Guid.
    /// </value>
    Guid MessageId { get; }

    /// <summary>
    ///     Gets the correlation identifier of the message.
    /// </summary>
    /// <value>
    ///     The correlation identifier of the message, represented as a nullable Guid.
    ///     This can be used to correlate messages that belong to the same operation or transaction.
    /// </value>
    Guid? CorrelationId { get; }

    /// <summary>
    ///     Gets the timestamp of the message.
    /// </summary>
    /// <value>
    ///     The timestamp represents the date and time when the message was created.
    ///     It is expressed as a DateTimeOffset, which represents a point in time,
    ///     typically expressed as a date and time of day, relative to Coordinated Universal Time (UTC).
    /// </value>
    DateTimeOffset Timestamp { get; }
}