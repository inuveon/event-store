using System.Reflection;
using Inuveon.EventStore.Abstractions.Storage;
using Inuveon.EventStore.Abstractions.Strategies;

namespace Inuveon.EventStore;

public class EventStoreOptions : IEventStoreOptions
{
    public string? StoreProvider { get; set; }
    public Func<Assembly, bool>? AssemblyFilter { get; set; }
    public Assembly[]? AssembliesToScan { get; set; }
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? ApplicationName { get; set; }
    public int Throughput { get; set; }
    public StoreStrategy StoreStrategy { get; set; } = StoreStrategy.OneStreamPerAggregate;
}