using Inuveon.EventStore.Abstractions.Strategies;

namespace Inuveon.EventStore.Abstractions.Storage;

/// <summary>
///     Represents the options for an event store.
/// </summary>
public interface IEventStoreOptions
{
    /// <summary>
    ///     Gets the connection string for the event store.
    /// </summary>
    string? ConnectionString { get; }

    /// <summary>
    ///     Gets the name of the database for the event store.
    /// </summary>
    string? DatabaseName { get; }

    /// <summary>
    ///     Gets the name of the application using the event store.
    /// </summary>
    string? ApplicationName { get; }

    /// <summary>
    ///     Gets the throughput for the event store.
    /// </summary>
    int Throughput { get; }

    /// <summary>
    ///     Gets the store strategy for the event store.
    /// </summary>
    StoreStrategy StoreStrategy { get; }
}