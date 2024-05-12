using System.Diagnostics;
using System.Reflection;
using Inuveon.EventStore.Abstractions.Entities;
using Inuveon.EventStore.Abstractions.Storage;

namespace Inuveon.EventStore;
public static class EventStoreConfigurator
{
    public static IEnumerable<EventStream> RegisterAggregateStreams(Assembly[] assembliesToScan)
    {
        var registeredStreams = new List<EventStream>();

        foreach (var assembly in assembliesToScan)
        {
            // Find all types that are classes, not abstract, and implement any IAggregateRoot<T>
            var aggregateTypes = assembly.GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false } && t.GetInterfaces().Contains(typeof(IAggregateRoot)))
                .ToList();

            // Add the detected aggregate root class names to the registered streams
            foreach (var type in aggregateTypes)
            {
                string streamName = type.Name.Replace("Aggregate", string.Empty);
                registeredStreams.Add(new EventStream(streamName));
                Debug.WriteLine($"Streams registered: {streamName}");
            }
            
            Debug.WriteLine($"Total streams detected: {registeredStreams.Count}");
        }

        return registeredStreams;
    }
}
