using EasyNetQ;
using EasyNetQ.Logging;

using Microsoft.Extensions.Hosting;

using TodoList.Shared.Models.Todo.QueueEvents;

namespace TodoList.BLL.HostedServices;

internal class QueueListener : IHostedService
{
    private readonly IBus _bus;
    private readonly ILogger<QueueListener> _logger;
    private SubscriptionResult _subscriptionResult;

    public QueueListener(IBus bus, ILogger<QueueListener> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _subscriptionResult = await _bus.PubSub.SubscribeAsync<TodoMessage>("TodoEventSubscription", x =>
        {
            _logger.Info("Todo '{0}', state '{1}'", x.Id, x.State);
        }, x =>
        {
            x.WithTopic("Todo");
        },
        cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _subscriptionResult.Dispose();
        return Task.CompletedTask;
    }
}