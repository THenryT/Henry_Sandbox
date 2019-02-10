using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDWithRabbitMQServiceBus.EventBus.Events;

namespace DDDWithRabbitMQServiceBus.EventBus.Abstract
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent: IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
