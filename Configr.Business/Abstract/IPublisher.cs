using Configr.Business.Queue;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configr.Business.Abstract
{
    public interface IPublisher
    {
        void Publish(ConfigurationDataWrapper configurationDataWrapper);
        EventingBasicConsumer Subscribe();
        IModel SubscribeChannel();
    }
}
