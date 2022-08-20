using Migrator.Attributes;
using Migrator.Commons.Logger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Migrator.Program
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            // new TypeMigrator(new SQLServerProviderNamespace.SQLServerProvider()).Migrate(new List<Type>() { typeof(Person), typeof(Pet) });
            new TypeMigrator(new SQLServerProviderNamespace.SQLServerProvider(new TSqlLogger())).Migrate(LoadAllBinDirectoryAssemblies());
        }

        private static List<Type> LoadAllBinDirectoryAssemblies()
        {
            List<Type> entityTypes = new List<Type>();
            foreach (string dll in Directory.GetFiles(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory), "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    Assembly loadedAssembly = Assembly.LoadFile(dll);
                    //if (AssemblyContainsEntity(loadedAssembly))
                    entityTypes.AddRange(loadedAssembly.GetTypes().Where(t => t.IsDefined(typeof(Entity))));    

                }
                catch (FileLoadException loadEx)
                { } // The Assembly has already been loaded.
                catch (BadImageFormatException imgEx)
                { } // If a BadImageFormatException exception is thrown, the file is not an assembly.

            } // foreach dll

            return entityTypes;
        }

        private static bool AssemblyContainsEntity(Assembly assembly)
        {

            var attributes = assembly.GetCustomAttributes(typeof(Entity), false);

            if (attributes.Length > 0)
                return true;
            

            return false;
        }
    }
}
