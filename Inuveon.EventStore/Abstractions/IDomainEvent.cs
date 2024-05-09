namespace Inuveon.EventStore.Abstractions;

public interface IDomainEvent : IMessage
{
    long Version { get; }
}