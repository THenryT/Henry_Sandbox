using System;
using System.Net.Sockets;
using System.Text;
using DDDWithRabbitMQServiceBus.EventBus;
using DDDWithRabbitMQServiceBus.EventBus.Abstract;
using DDDWithRabbitMQServiceBus.EventBus.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace DDDWithRabbitMQServiceBus.EventBusRabbitMQ
{
    public class EventBusRabbitMq : IEventBus
    {
        private const string BrokerName = "test_broker";
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly int _retryCount;
        private readonly IEventBusSubscriptionsManager _subsManager;

        public EventBusRabbitMq(IRabbitMqPersistentConnection rabbitMqPersistentConnection, EventBus.IEventBusSubscriptionsManager subsManager
            ,int retryCount = 5)
        {
            _retryCount = retryCount;
            _persistentConnection = rabbitMqPersistentConnection;
            _subsManager = subsManager;
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected) _persistentConnection.TryConnect();

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            using (var channel = _persistentConnection.CreateModel())
            {
                var eventName = @event.GetType()
                    .Name;

                channel.ExchangeDeclare(BrokerName,
                    "direct");

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                policy.Execute(() =>
                {
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2; // persistent

                    channel.BasicPublish(BrokerName,
                        eventName,
                        true,
                        properties,
                        body);
                });
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);
            _subsManager.AddSubscription<T, TH>();
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using (var channel = _persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: "test",
                        exchange: BrokerName,
                        routingKey: eventName);
                }
            }
        }
    }
}