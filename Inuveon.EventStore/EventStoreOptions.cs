using System.Reflection;
using Inuveon.EventStore.Abstractions.Strategies;

namespace Inuveon.EventStore;

public class EventStoreOptions
{
    public StoreStrategy StoreStrategy { get; set; } = StoreStrategy.OneStreamPerAggregate;
    public string StoreProvider { get; set; }
    public Func<Assembly, bool> AssemblyFilter { get; set; }
    public Assembly[] AssembliesToScan { get; set; }
}