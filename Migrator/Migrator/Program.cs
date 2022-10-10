using Autofac;
using Migrator.DI;
using System;
using System.Globalization;
using System.Threading;

namespace Migrator.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                IApplication app = scope.Resolve<IApplication>();
                app.Run(args[0]);
            }
        }
    }
}
