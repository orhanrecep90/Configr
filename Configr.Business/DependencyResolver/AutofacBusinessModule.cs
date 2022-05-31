using Autofac;
using Configr.Business.Abstract;
using Configr.Business.Concrete;
using Configr.DataAccess.Abstract;
using Configr.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configr.Business.DependencyResolver
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationDatasService>().As<IConfigurationDatasService>();
            builder.RegisterType<EfConfigurationDatasDal>().As<IConfigurationDatasDal>();
        }
    }
}
