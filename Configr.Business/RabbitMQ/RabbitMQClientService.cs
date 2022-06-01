using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configr.Business.Queue
{

    public class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "ConfigurationDatasFanoutExchange";

        public RabbitMQClientService()
        {
            _connectionFactory = new ConnectionFactory {Uri =new Uri("amqps://xespwuhy:X_rdI5Ex2dCPsRftUlPC8vg0Yxisg6If@rattlesnake.rmq.cloudamqp.com/xespwuhy") };
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, durable: true, type: ExchangeType.Fanout);
            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();


        }
    }
}
