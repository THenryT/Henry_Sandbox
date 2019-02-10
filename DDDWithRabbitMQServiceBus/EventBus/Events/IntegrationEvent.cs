using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDWithRabbitMQServiceBus.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}
