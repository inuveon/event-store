using System.Text.Json.Serialization;
using FluentValidation;
using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Abstractions.Entities
{
    /// <summary>
    /// Represents an abstract base class for an aggregate root with a specific validator.
    /// </summary>
    /// <typeparam name="TValidator">The type of the validator.</typeparam>
    public abstract class AggregateRoot<TValidator> : IAggregateRoot where TValidator : IValidator, new()
    {
        private readonly List<IDomainEvent> _events = new();

        /// <summary>
        /// Gets the unique identifier of the aggregate root.
        /// </summary>
        public Guid Id { get;  }

        /// <summary>
        /// Gets the version of the aggregate root.
        /// </summary>
        public long Version { get; private set; }

        /// <summary>
        /// Gets the uncommitted events of the aggregate root.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<IDomainEvent> UncommittedEvents => _events.AsReadOnly();

        /// <summary>
        /// Loads the aggregate root from a history of domain events.
        /// </summary>
        /// <param name="events">The history of domain events.</param>
        public void LoadFromHistory(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
                Version = @event.Version;
            }
        }

        /// <summary>
        /// Handles a command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        public abstract void Handle(ICommand command);

        /// <summary>
        /// Raises a domain event.
        /// </summary>
        /// <typeparam name="TEvent">The type of the domain event.</typeparam>
        /// <param name="func">A function that creates the domain event.</param>
        protected void RaiseEvent<TEvent>(Func<long, TEvent> func) where TEvent : IDomainEvent
            => RaiseEvent((func as Func<long, IDomainEvent>)!);

        /// <summary>
        /// Raises a domain event.
        /// </summary>
        /// <param name="onRaise">A function that creates the domain event.</param>
        protected void RaiseEvent(Func<long, IDomainEvent> onRaise)
        {
            Version++;
            var @event = onRaise(Version);
            Apply(@event);
            Validate();
            _events.Add(@event);
        }

        /// <summary>
        /// Applies a domain event to the aggregate root.
        /// </summary>
        /// <param name="event">The domain event to apply.</param>
        protected abstract void Apply(IDomainEvent @event);

        /// <summary>
        /// Validates the aggregate root.
        /// </summary>
        private void Validate()
        {
            new TValidator()
                .Validate(ValidationContext<IEntity>
                    .CreateWithOptions(this, strategy => strategy.ThrowOnFailures()));
        }
    }
}
