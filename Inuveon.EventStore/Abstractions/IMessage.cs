namespace Inuveon.EventStore.Abstractions;

public interface IMessage
{
    Guid Id { get; }
    DateTimeOffset Timestamp { get; }
}