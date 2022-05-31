using Configr.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configr.DataAccess.Concrete.EntityFramework.Contexts
{
    public class ConfigrContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Configr;Integrated Security=True");
        }
        public DbSet<ConfigurationDatas> ConfigurationDatas { get; set; }
    }
}
