using System.Text;
using System.Text.Json;
using Inuveon.EventStore.Converters;
using Inuveon.EventStore.Abstractions;

namespace Inuveon.EventStore.Tests.Converters;

public class DomainEventJsonConverterTests
{
    private string ValidJson => "{\"TypeDiscriminator\":\"" + typeof(DummyEvent).AssemblyQualifiedName + "\",\"Id\":\"00000000-0000-0000-0000-000000000000\",\"Timestamp\":\"0001-01-01T00:00:00+00:00\",\"Version\":0}";
    private class DummyEvent : IDomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
        public long Version { get; } = 0;
    }

    [Fact]
    public void Read_ValidJson_ReturnsDomainEvent()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new DomainEventJsonConverter() }
        };
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(ValidJson));
        var converter = new DomainEventJsonConverter();

        var result = converter.Read(ref reader, typeof(IDomainEvent), options);

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IDomainEvent>(result);
    }

    [Fact]
    public void Read_InvalidTypeDiscriminator_ThrowsJsonException()
    {
        var options = new JsonSerializerOptions();
        var converter = new DomainEventJsonConverter();

        Assert.Throws<JsonException>(() =>
        {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes("{\"TypeDiscriminator\":\"InvalidType\"}"));
            return converter.Read(ref reader, typeof(IDomainEvent), options);
        });
    }

    [Fact]
    public void Write_ValidDomainEvent_WritesExpectedJson()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new DomainEventJsonConverter() }
        };

        using var memoryStream = new MemoryStream();
        using var writer = new Utf8JsonWriter(memoryStream, new JsonWriterOptions { Indented = true });
        var converter = new DomainEventJsonConverter();
        var dummyEvent = new DummyEvent();

        converter.Write(writer, dummyEvent, options);
        writer.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);
        var json = Encoding.UTF8.GetString(memoryStream.ToArray());

        // Parse the JSON string back into a JsonDocument to inspect it more flexibly
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        // Assert that the JSON contains the TypeDiscriminator with the correct value
        Assert.True(root.TryGetProperty("TypeDiscriminator", out var typeDiscriminatorProp));
        var expectedTypeName = typeof(DummyEvent).AssemblyQualifiedName;
        Assert.Equal(expectedTypeName, typeDiscriminatorProp.GetString());
    }


}

