using Configr.Business.Abstract;
using Configr.Business.Queue;
using Configr.DataAccess.Abstract;
using Configr.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configr.Business.Concrete
{
    public class ConfigurationDatasService : IConfigurationDatasService
    {
        private readonly IConfigurationDatasDal _configurationDatasDal;
        private readonly IPublisher _publisher;

        public ConfigurationDatasService(IConfigurationDatasDal configurationDatasDal, IPublisher publisher)
        {
            _configurationDatasDal = configurationDatasDal;
            _publisher = publisher;
        }

        public async Task<ConfigurationDatas> Add(ConfigurationDatas configurationDatas)
        {
       
            await _configurationDatasDal.Add(configurationDatas);
            _publisher.Publish(new ConfigurationDataWrapper { ConfigurationDatas = configurationDatas, Operation = "C" });
            return configurationDatas;
        }

        public async Task<ConfigurationDatas> Delete(int ID)
        {
            var data = await this.GetById(ID);
            await _configurationDatasDal.Delete(data);
            _publisher.Publish(new ConfigurationDataWrapper { ConfigurationDatas = data, Operation = "D" });
            return data;
        }

        public async Task<ConfigurationDatas> GetById(int id)
        {
            return await _configurationDatasDal.Get(x => x.ID == id);
        }

        public async Task<List<ConfigurationDatas>> GetAll()
        {
            return await _configurationDatasDal.GetAll();
        }

        public async Task<List<ConfigurationDatas>> GetAllByApplicationName(string name)
        {
            return await _configurationDatasDal.GetAll(x => x.ApplicationName == name && x.IsActive);
        }
        public async Task<List<ConfigurationDatas>> GetAllByName(string name)
        {
            return await _configurationDatasDal.GetAll(x => x.Name == name);
        }

        public async Task<ConfigurationDatas> Update(ConfigurationDatas configurationDatas)
        {
            await _configurationDatasDal.Update(configurationDatas);
            _publisher.Publish(new ConfigurationDataWrapper { ConfigurationDatas = configurationDatas, Operation = "U" });
            return configurationDatas;
        }
    }
}
