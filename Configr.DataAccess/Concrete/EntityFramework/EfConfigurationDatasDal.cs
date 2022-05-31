using Configr.Core.DataAccess;
using Configr.DataAccess.Abstract;
using Configr.DataAccess.Concrete.EntityFramework.Contexts;
using Configr.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configr.DataAccess.Concrete.EntityFramework
{
    public class EfConfigurationDatasDal:EntityRepositoryBase<ConfigurationDatas, ConfigrContext>, IConfigurationDatasDal
    {
    }
}
