using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Abstractions.Entities;

/// <summary>
///     Represents an aggregate root in the domain-driven design context.
///     An aggregate root is an entity that other entities have a reference to.
/// </summary>
public interface IAggregateRoot : IEntity
{
    /// <summary>
    ///     Gets the collection of uncommitted domain events.
    ///     These are events that have occurred but have not yet been persisted to the event store.
    /// </summary>
    IEnumerable<IDomainEvent> UncommittedEvents { get; }

    /// <summary>
    ///     Loads the aggregate root from a history of domain events.
    /// </summary>
    /// <param name="events">The history of domain events.</param>
    void LoadFromHistory(IEnumerable<IDomainEvent> events);

    /// <summary>
    ///     Handles a command.
    ///     A command is an instruction to do something, which results in domain events.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    void Handle(ICommand command);
}