using System.Text.Json;
using Inuveon.EventStore.Converters;
using Microsoft.Azure.Cosmos;

namespace Inuveon.EventStore.CosmosDb;

public class CustomCosmosSerializer : CosmosSerializer
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = { new DomainEventJsonConverter() } 
    };

    public override T FromStream<T>(Stream stream)
    {
        if (stream is { CanSeek: true, Length: 0 })
        {
            // Explicitly return default, knowing it could be null.
            return default!;
        }

        using (StreamReader reader = new StreamReader(stream))
        using (JsonDocument document = JsonDocument.Parse(reader.ReadToEnd()))
        {
            string json = document.RootElement.GetRawText();
            T? result = JsonSerializer.Deserialize<T>(json, _options);

            if (result == null)
            {
                // Throw an exception if deserialization returns null but null is not expected
                throw new InvalidOperationException("Deserialization returned null.");
            }

            return result;
        }
    }

    public override Stream ToStream<T>(T input)
    {
        MemoryStream stream = new MemoryStream();
        using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
        {
            JsonSerializer.Serialize(writer, input, _options);
        }
        stream.Position = 0;
        return stream;
    }
}
