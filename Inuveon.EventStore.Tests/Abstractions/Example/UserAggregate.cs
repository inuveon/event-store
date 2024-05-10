using Inuveon.EventStore.Abstractions;

namespace Inuveon.EventStore.Tests.Abstractions.Example;

public class UserAggregate : AggregateRoot<UserValidator>
{
    public new Guid Id { get; private set; }
    public string? FirstName { get; private set; }
    public string? Email { get; private set; }
    
    // Commands.
    public override void Handle(ICommand command) 
        => Handle(command as dynamic);

    private void Handle(RegisterUserCommand cmd)
        => RaiseEvent<UserRegistered>(version
            => new(Guid.NewGuid(), Guid.NewGuid(), cmd.FirstName, cmd.EmailAddress, DateTimeOffset.Now, version,
                cmd.CorrelationId, cmd.MessageId));

    
    // Events.
    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(UserRegistered @event)
    {
        Id = @event.AggregateId;
        FirstName = @event.FirstName;
        Email = @event.EmailAddress;
    }
    
    private void When(UserNameChanged @event)
    {
        FirstName = @event.FirstName;
    }
}