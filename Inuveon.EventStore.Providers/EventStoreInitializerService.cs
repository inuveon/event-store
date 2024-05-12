using Inuveon.EventStore.Abstractions.Storage;
using Microsoft.Extensions.Hosting;

namespace Inuveon.EventStore.Providers;

public class EventStoreInitializerService(IEventStoreProviderInitializer initializer) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return initializer.InitializeAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}