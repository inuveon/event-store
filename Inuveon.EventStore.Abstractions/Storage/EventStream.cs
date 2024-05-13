namespace Inuveon.EventStore.Abstractions.Storage;

public record EventStream(string StreamName, string PartitionKeyPath = "/streamId", string SortKey = "/sequenceNumber");