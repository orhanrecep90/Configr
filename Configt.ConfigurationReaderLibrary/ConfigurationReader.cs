using Autofac;
using Configr.Business.Abstract;
using Configr.Business.Concrete;
using Configr.Business.Queue;
using Configr.DataAccess.Concrete.EntityFramework;
using Configr.Entities.Concrete;
using Configt.ConfigurationReaderLibrary.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Configt.ConfigurationReaderLibrary
{
    public class ConfigurationReader
    {
        private readonly string _applicationName;
        private readonly string _connectionString;
        private readonly int _refreshTimerIntervalInMs;
        private readonly CacheDto<ConfigurationDatas> _cacheDto;
        private readonly IConfigurationDatasService _configurationDatasService;
        private readonly IPublisher _publisher;

        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {

            _applicationName = applicationName;
            _connectionString = connectionString;
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;
            _cacheDto = new CacheDto<ConfigurationDatas>();
            _configurationDatasService = new ConfigurationDatasService(new EfConfigurationDatasDal(), new RabbitMQPublisher());
            _publisher = new RabbitMQPublisher();
            Subscribe();


        }
        private async Task GetDataFromDBAsync()
        {
            _cacheDto.ValidityDate = DateTime.Now.AddMilliseconds(_refreshTimerIntervalInMs);
            _cacheDto.List = await _configurationDatasService.GetAllByApplicationName(_applicationName);

        }

        public async Task<T> GetValue<T>(string key)
        {
            if (_cacheDto.List == null || _cacheDto.List.Count == 0)
                await GetDataFromDBAsync();

            if (_cacheDto.ValidityDate < DateTime.Now)
                await GetDataFromDBAsync();

            var cacheData = _cacheDto.List?.FirstOrDefault(x =>
             x.ApplicationName == _applicationName &&
             x.IsActive &&
             x.Name == key
            );
            if(cacheData == null)
                return default(T);

            var cahceValue = (T)Convert.ChangeType(cacheData.Value, typeof(T));
            return cahceValue;
        }
        public void Subscribe()
        {
            var channel = _publisher.SubscribeChannel();
            var randomQueueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(randomQueueName, RabbitMQClientService.ExchangeName, "", null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(randomQueueName, false, consumer);
            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
              {
                  var message = Encoding.UTF8.GetString(e.Body.ToArray());
                  ConfigurationDataWrapper data = JsonSerializer.Deserialize<ConfigurationDataWrapper>(message);
                  RefreshData(data);
                  channel.BasicAck(e.DeliveryTag, false);
              };
        }

        private void RefreshData(ConfigurationDataWrapper configurationDataWrapper)
        {
            switch (configurationDataWrapper.Operation)
            {
                case "C":
                    _cacheDto.List.Add(configurationDataWrapper.ConfigurationDatas);
                break;
                case "U":
                    _cacheDto.List.Remove(_cacheDto.List.FirstOrDefault(x => x.ID == configurationDataWrapper.ConfigurationDatas.ID));
                    _cacheDto.List.Add(configurationDataWrapper.ConfigurationDatas);
                    break;
                case "D":
                    _cacheDto.List.Remove(_cacheDto.List.FirstOrDefault(x => x.ID == configurationDataWrapper.ConfigurationDatas.ID));
                    break;
                default:
                    break;
            }
        }
    }
}
