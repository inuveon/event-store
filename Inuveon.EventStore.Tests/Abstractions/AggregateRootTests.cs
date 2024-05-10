using FluentValidation;
using Inuveon.EventStore.Abstractions;
using Inuveon.EventStore.Tests.Abstractions.Example;

namespace Inuveon.EventStore.Tests.Abstractions;

public class AggregateRootTests
{
    [Fact]
    public void LoadFromHistory_AppliesEventsCorrectly()
    {
        var updatedFirstName = TestDataGenerator.RandomFirstName();
        var emailAddress = TestDataGenerator.RandomEmail();
        var aggregateId = Guid.NewGuid();
        
        var aggregate = new UserAggregate();
        var events = new List<IDomainEvent>
        {
            new UserRegistered(Guid.NewGuid(), aggregateId, TestDataGenerator.RandomFirstName(), emailAddress, DateTimeOffset.Now, 1, Guid.NewGuid(), Guid.NewGuid()),
            new UserNameChanged(Guid.NewGuid(), aggregateId, updatedFirstName, DateTimeOffset.Now, 2, Guid.NewGuid(), Guid.NewGuid())
        };
    
        aggregate.LoadFromHistory(events);
    
        Assert.Equal(2, aggregate.Version);
        Assert.Equal(aggregateId, aggregate.Id);
        Assert.Equal(updatedFirstName, aggregate.FirstName);
        Assert.Equal(emailAddress, aggregate.Email);
        Assert.Equal(2, aggregate.Version);
    }
    
    [Fact]
    public void Handle_Command_RaisesEvent()
    {
        var aggregate = new UserAggregate();
        var command =  new RegisterUserCommand(Guid.NewGuid(), TestDataGenerator.RandomFirstName(), TestDataGenerator.RandomEmail(), DateTimeOffset.Now, 0, Guid.NewGuid());
    
        aggregate.Handle(command);
    
        Assert.Single(aggregate.UncommittedEvents);
        Assert.Equal(1, aggregate.Version);
    }

   
    [Fact]
    public void ShouldThrowValidationException_WhenFirstNameIsMissing()
    {
        var aggregate = new UserAggregate();
        var command = new RegisterUserCommand(Guid.NewGuid(), "", TestDataGenerator.RandomEmail(), DateTimeOffset.Now, 0, Guid.NewGuid());

        var exception = Record.Exception(() => aggregate.Handle(command));

        Assert.IsType<ValidationException>(exception);
    }
    
    [Fact]
    public void ShouldThrowValidationException_WhenEmailIsMissing()
    {
        var aggregate = new UserAggregate();
        var command = new RegisterUserCommand(Guid.NewGuid(), TestDataGenerator.RandomFirstName(), "", DateTimeOffset.Now, 0, Guid.NewGuid());

        var exception = Record.Exception(() => aggregate.Handle(command));

        Assert.IsType<ValidationException>(exception);
    }
    
    [Fact]
    public void ShouldThrowValidationException_WhenEmailIsNotValid()
    {
        var aggregate = new UserAggregate();
        var command = new RegisterUserCommand(Guid.NewGuid(), TestDataGenerator.RandomFirstName(), "wrong", DateTimeOffset.Now, 0, Guid.NewGuid());

        var exception = Record.Exception(() => aggregate.Handle(command));

        Assert.IsType<ValidationException>(exception);
    }
}

public class UserValidator : AbstractValidator<UserAggregate>
{
    public UserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
    }
}