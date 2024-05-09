namespace Inuveon.EventStore.Abstractions;

public interface IAggregateRoot : IEntity
{
    IEnumerable<IDomainEvent> UncommittedEvents { get; }
    void LoadFromHistory(IEnumerable<IDomainEvent> events);
    void Handle(ICommand command);
}