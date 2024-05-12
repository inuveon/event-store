using FluentValidation;
using Inuveon.EventStore.Abstractions.Entities;
using Inuveon.EventStore.Abstractions.Exceptions;
using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Tests.Exceptions;

public class AggregateNotFoundExceptionTests
{
    [Fact]
    public void AggregateNotFoundException_ShouldReturnCorrectMessage_WhenToStringIsCalled()
    {
        // Arrange
        var aggregateId = Guid.NewGuid();
        var aggregateType = typeof(DummyAggregate);
        var message = "Test message";
        var expectedMessage =
            $"AggregateNotFoundException: {message} - Aggregate ID: {aggregateId}, Aggregate Type: {aggregateType.Name}";

        // Act
        var exception = new AggregateNotFoundException(aggregateId, aggregateType, message);
        var actualMessage = exception.ToString();

        // Assert
        Assert.Equal(expectedMessage, actualMessage);
    }

    [Fact]
    public void AggregateNotFoundException_ShouldContainCorrectMessage_WhenCreatedWithMessage()
    {
        // Arrange
        var aggregateId = Guid.NewGuid();
        var aggregateType = typeof(DummyAggregate);
        var message = "Test message";

        // Act
        var exception = new AggregateNotFoundException(aggregateId, aggregateType, message);

        // Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public void AggregateNotFoundException_ShouldContainCorrectAggregateId_WhenCreatedWithAggregateId()
    {
        // Arrange
        var aggregateId = Guid.NewGuid();
        var aggregateType = typeof(DummyAggregate);
        var message = "Test message";

        // Act
        var exception = new AggregateNotFoundException(aggregateId, aggregateType, message);

        // Assert
        Assert.Equal(aggregateId, exception.AggregateId);
    }

    [Fact]
    public void AggregateNotFoundException_ShouldContainCorrectAggregateType_WhenCreatedWithAggregateType()
    {
        // Arrange
        var aggregateId = Guid.NewGuid();
        var aggregateType = typeof(DummyAggregate);
        var message = "Test message";

        // Act
        var exception = new AggregateNotFoundException(aggregateId, aggregateType, message);

        // Assert
        Assert.Equal(aggregateType, exception.AggregateType);
    }
}

public class DummyAggregate : AggregateRoot<DummyAggregateValidator>
{
    public override void Handle(ICommand command)
    {
        throw new NotImplementedException();
    }

    protected override void Apply(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}

public class DummyAggregateValidator : AbstractValidator<DummyAggregate>
{
}