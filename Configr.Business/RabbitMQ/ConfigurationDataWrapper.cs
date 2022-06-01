using Configr.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configr.Business.Queue
{
    public class ConfigurationDataWrapper
    {
        public string Operation { get; set; }
        public ConfigurationDatas ConfigurationDatas { get; set; }
    }
}
