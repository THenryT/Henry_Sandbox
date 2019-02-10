using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDWithRabbitMQServiceBus.EventBus.Abstract;

namespace DDDWithRabbitMQServiceBus.Events.TestEventHandler
{
    public class TestEventHandler: IIntegrationEventHandler<TestEvent>
    {
        public bool Handle(TestEvent @event)
        {
            return true;
        }
    }
}
