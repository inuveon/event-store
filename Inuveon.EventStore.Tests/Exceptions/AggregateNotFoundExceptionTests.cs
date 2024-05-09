namespace Inuveon.EventStore.Tests.Exceptions;

using Xunit;
using System;
using Inuveon.EventStore.Exceptions;

public class AggregateNotFoundExceptionTests
{
    [Fact]
    public void AggregateNotFoundException_WithValidInputs_ShouldSetMessageCorrectly()
    {
        // Arrange
        Guid aggregateId = Guid.NewGuid();
        Type type = typeof(string);

        // Act
        var exception = new AggregateNotFoundException(aggregateId, type);

        // Assert
        Assert.Equal($"Aggregate {type.Name} with id {aggregateId} not found", exception.Message);
    }

    [Fact]
    public void AggregateNotFoundException_WithNullType_ShouldThrowNullReferenceException()
    {
        // Arrange
        Guid aggregateId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => new AggregateNotFoundException(aggregateId, null!));
    }
}