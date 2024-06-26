using System.Text.Json;
using System.Text.Json.Serialization;
using Inuveon.EventStore.Abstractions.Messages;

namespace Inuveon.EventStore.Abstractions.Converters;

public class DomainEventJsonConverter : JsonConverter<IDomainEvent>
{
    public override IDomainEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var doc = JsonDocument.ParseValue(ref reader);
        var typeDiscriminator = doc.RootElement.GetProperty("TypeDiscriminator").GetString();
        if (typeDiscriminator == null) throw new JsonException("TypeDiscriminator is missing");

        var type = Type.GetType(typeDiscriminator);
        if (type == null) throw new JsonException($"Unknown type discriminator: {typeDiscriminator}");

        return (IDomainEvent)JsonSerializer.Deserialize(doc.RootElement.GetRawText(), type, options)!;
    }

    public override void Write(Utf8JsonWriter writer, IDomainEvent value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("TypeDiscriminator", value.GetType().AssemblyQualifiedName);
        foreach (var prop in value.GetType().GetProperties())
        {
            writer.WritePropertyName(prop.Name);
            JsonSerializer.Serialize(writer, prop.GetValue(value), prop.PropertyType, options);
        }

        writer.WriteEndObject();
    }
}