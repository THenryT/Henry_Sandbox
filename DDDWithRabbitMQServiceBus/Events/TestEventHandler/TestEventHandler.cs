using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDWithRabbitMQServiceBus.EventBus.Abstract;

namespace DDDWithRabbitMQServiceBus.Events.TestEventHandler
{
    public class TestEventHandler: IIntegrationEventHandler<TestEvent>
    {
        public async Task Handle(TestEvent @event)
        {
            await Task.Run(() =>
            {
                var a = "good";
            });
        }
    }
}
