using System.Text;
using Inuveon.EventStore.CosmosDb;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Inuveon.EventStore.Providers.Tests.CosmosDbNoSql;

public class TestEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class AnotherEntity
{
    public Guid Id { get; set; }
}


public class CustomCosmosSerializerTests
{
    [Fact]
    public void FromStream_ReturnsNull_WhenStreamIsEmpty()
    {
        // Arrange
        var serializer = new CustomCosmosSerializer();
        using var emptyStream = new MemoryStream();

        // Act
        var result = serializer.FromStream<TestEntity>(emptyStream);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void FromStream_ThrowsInvalidOperationException_WhenDeserializationFails()
    {
        // Arrange
        var serializer = new CustomCosmosSerializer();
        var entity = new AnotherEntity { Id = Guid.NewGuid() };
        
        using var invalidStream = serializer.ToStream(entity);
        using var reader = new StreamReader(invalidStream);
        var invalidJson = reader.ReadToEnd();
        
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJson));

        // Act & Assert
        Assert.Throws<System.Text.Json.JsonException>(() => serializer.FromStream<TestEntity>(stream));
    }

    [Fact]
    public void FromStream_DeserializesCorrectly_WhenJsonIsValid()
    {
        // Arrange
        var serializer = new CustomCosmosSerializer();
        var entity = new TestEntity { Id = 1, Name = "Test Name" };
        var json = JsonSerializer.Serialize(entity);
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        // Act
        var result = serializer.FromStream<TestEntity>(stream);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal(entity.Name, result.Name);
    }

    [Fact]
    public void ToStream_SerializesCorrectly()
    {
        // Arrange
        var serializer = new CustomCosmosSerializer();
        var entity = new TestEntity { Id = 1, Name = "Test Name" };

        // Act
        using var stream = serializer.ToStream(entity);
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        var deserialized = JsonSerializer.Deserialize<TestEntity>(json);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(entity.Id, deserialized.Id);
        Assert.Equal(entity.Name, deserialized.Name);
    }
}
