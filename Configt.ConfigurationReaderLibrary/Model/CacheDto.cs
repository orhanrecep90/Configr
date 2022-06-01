using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configt.ConfigurationReaderLibrary.Model
{
    public class CacheDto<T>
    {
        public List<T> List { get; set; }
        public DateTime ValidityDate { get; set; }
    }
}
