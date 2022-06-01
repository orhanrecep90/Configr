﻿using Configr.Business.Abstract;
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

        public ConfigurationDatasService(IConfigurationDatasDal configurationDatasDal)
        {
            _configurationDatasDal = configurationDatasDal;
        }

        public async Task Add(ConfigurationDatas configurationDatas)
        {
            if (configurationDatas.IsActive)
            {
                var sameNameActiveData = (await _configurationDatasDal.GetAll(x => x.ApplicationName == configurationDatas.ApplicationName && x.IsActive==true)).FirstOrDefault();
                if (sameNameActiveData != null)
                {
                    sameNameActiveData.IsActive = false;
                   await this.Update(sameNameActiveData);
                }
            }
            await _configurationDatasDal.Add(configurationDatas);
        }

        public async Task Delete(int ID)
        {
            var data =await this.GetById(ID);
            await _configurationDatasDal.Delete(data);
        }

        public async Task<ConfigurationDatas> GetById(int id)
        {
            return await _configurationDatasDal.Get(x => x.ID == id);
        }

        public async Task<List<ConfigurationDatas>> GetAll()
        {
            return await _configurationDatasDal.GetAll();
        }

        public async Task<List<ConfigurationDatas>> GetAllByName(string name)
        {
            return await _configurationDatasDal.GetAll(x => x.Name == name);
        }

        public async Task Update(ConfigurationDatas configurationDatas)
        {
            if (configurationDatas.IsActive)
            {
                var sameNameActiveData = (await _configurationDatasDal.GetAll(x => x.ApplicationName == configurationDatas.ApplicationName && x.IsActive == true)).FirstOrDefault();
                if (sameNameActiveData != null)
                {
                    sameNameActiveData.IsActive = false;
                    await _configurationDatasDal.Update(sameNameActiveData);
                }
            }
            await _configurationDatasDal.Update(configurationDatas);
        }
    }
}
