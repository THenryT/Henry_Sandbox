using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDWithRabbitMQServiceBus.EventBus.Events;

namespace DDDWithRabbitMQServiceBus.EventBus.Abstract
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}
