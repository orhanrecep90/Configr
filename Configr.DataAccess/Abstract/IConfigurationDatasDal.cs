using Configr.Core.DataAccess;
using Configr.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configr.DataAccess.Abstract
{
    public interface IConfigurationDatasDal:IEntityRepository<ConfigurationDatas>
    {
    }
}
