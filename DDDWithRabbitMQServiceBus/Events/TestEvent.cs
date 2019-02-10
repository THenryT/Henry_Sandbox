using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDWithRabbitMQServiceBus.EventBus.Events;

namespace DDDWithRabbitMQServiceBus.Events
{
    public class TestEvent: IntegrationEvent
    {
        public TestEvent(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
}
