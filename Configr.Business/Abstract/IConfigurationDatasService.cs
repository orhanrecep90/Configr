using Configr.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configr.Business.Abstract
{
    public interface IConfigurationDatasService
    {
        Task<ConfigurationDatas> GetById(int ID);
        Task<List<ConfigurationDatas>> GetAll();
        Task<List<ConfigurationDatas>> GetAllByName(string name);
        Task<List<ConfigurationDatas>> GetAllByApplicationName(string name);
        Task<ConfigurationDatas> Add(ConfigurationDatas configurationDatas);
        Task<ConfigurationDatas> Update(ConfigurationDatas configurationDatas);
        Task<ConfigurationDatas> Delete(int ID);
    }
}
