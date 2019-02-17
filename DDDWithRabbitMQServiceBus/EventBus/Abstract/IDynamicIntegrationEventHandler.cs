using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDWithRabbitMQServiceBus.EventBus.Abstract
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
