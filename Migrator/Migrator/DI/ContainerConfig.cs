using Autofac;
using Migrator.Commons.Logger;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.DI
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ApplicationTest>().As<IApplication>();
            builder.RegisterType<TypeMigrator>().As<ITypeMigrator>();
            builder.RegisterType<SQLServerProvider>().As<ISQLProvider>();
            builder.RegisterType<TSqlLogger>().As<ILogger>();
            return builder.Build();
        }
    }
}
