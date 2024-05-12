using System.Reflection;

namespace Inuveon.EventStore;

public class EventStoreOptions
{
    public string StoreProvider { get; set; }
    public Func<Assembly, bool> AssemblyFilter { get; set; }
    public Assembly[] AssembliesToScan { get; set; }
}