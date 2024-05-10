using Inuveon.EventStore.Abstractions;
using Moq;

namespace Inuveon.EventStore.Tests.Abstractions
{
    public class StoreEventTests
    {
        [Fact]
        public void Create_ShouldReturnStoreEvent_WhenValidParametersArePassed()
        {
            // Arrange
            var aggregate = new Mock<IAggregateRoot>();
            var domainEvent = new Mock<IDomainEvent>();

            // Act
            var result = StoreEvent.Create(aggregate.Object, domainEvent.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(domainEvent.Object.MessageId, result.Id);
            Assert.Equal(domainEvent.Object.AggregateId, result.StreamId);
            Assert.Equal(domainEvent.Object.GetType().FullName, result.EventType);
            Assert.Equal(domainEvent.Object, result.Data);
            Assert.Equal(domainEvent.Object.Timestamp, result.Timestamp);
            Assert.Equal(domainEvent.Object.Version, result.Version);
        }

        [Fact]
        public void Create_ShouldThrowNullReferenceException_WhenAggregateIsNull()
        {
            // Arrange
            var domainEvent = new Mock<IDomainEvent>();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => StoreEvent.Create(null!, domainEvent.Object));
        }

        [Fact]
        public void Create_ShouldThrowNullReferenceException_WhenDomainEventIsNull()
        {
            // Arrange
            var aggregate = new Mock<IAggregateRoot>();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => StoreEvent.Create(aggregate.Object, null!));
        }
    }
}