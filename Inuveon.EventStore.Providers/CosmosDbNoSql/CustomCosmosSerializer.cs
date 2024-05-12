using System.Text.Json;
using Inuveon.EventStore.Abstractions.Converters;
using Microsoft.Azure.Cosmos;

namespace Inuveon.EventStore.Providers.CosmosDbNoSql
{
    /// <summary>
    /// Represents a custom serializer for Cosmos DB that uses the System.Text.Json library for serialization and deserialization.
    /// </summary>
    public class CustomCosmosSerializer : CosmosSerializer
    {
        /// <summary>
        /// The options for the JSON serializer.
        /// </summary>
        private readonly JsonSerializerOptions _options = new()
        {
            Converters = { new DomainEventJsonConverter() }
        };

        /// <summary>
        /// Deserializes the JSON data from the provided stream into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="stream">The stream that contains the JSON data to deserialize.</param>
        /// <returns>The deserialized object from the stream.</returns>
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

        /// <summary>
        /// Serializes the specified object to a JSON string and writes it to a stream.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="input">The object to serialize.</param>
        /// <returns>A stream that contains the serialized JSON data.</returns>
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
}