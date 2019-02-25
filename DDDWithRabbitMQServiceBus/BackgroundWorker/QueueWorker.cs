using System.Threading;
using System.Threading.Tasks;
using DDDWithRabbitMQServiceBus.EventBus.Abstract;
using Microsoft.Extensions.Hosting;

namespace DDDWithRabbitMQServiceBus.BackgroundWorker
{
    public class QueueWorker : IHostedService
    {
        private readonly IEventBus _eventBus;

        public QueueWorker(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var channel = _eventBus.CreateConsumerChannel();
            channel.BasicGet("test", true);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}