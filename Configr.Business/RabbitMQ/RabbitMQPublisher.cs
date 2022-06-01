using Configr.Business.Abstract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Configr.Business.Queue
{
    public class RabbitMQPublisher:IPublisher
    {
        private readonly RabbitMQClientService _rabbitmqClientService;

        public RabbitMQPublisher()
        {
            _rabbitmqClientService = new RabbitMQClientService() ;
        }
        public void Publish(ConfigurationDataWrapper configurationDataWrapper)
        {
            var channel = _rabbitmqClientService.Connect();
            var serializedData = JsonSerializer.Serialize(configurationDataWrapper);
            var messageBody = Encoding.UTF8.GetBytes(serializedData);
            //var properties = channel.CreateBasicProperties();
            //properties.Persistent = true;


            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: "", null, body: messageBody);
        }

        public EventingBasicConsumer Subscribe()
        {
            var channel = _rabbitmqClientService.Connect();
            var randomQueueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(randomQueueName, RabbitMQClientService.ExchangeName, "", null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(randomQueueName, false, consumer);
            return consumer;
        }

        public IModel SubscribeChannel()
        {
            var channel = _rabbitmqClientService.Connect();
            return channel;

           
        
        }
    }
}
