using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDWithRabbitMQServiceBus.EventBus.Events;
using RabbitMQ.Client;

namespace DDDWithRabbitMQServiceBus.EventBus.Abstract
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        IModel CreateConsumerChannel();
    }
}
