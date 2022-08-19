using Migrator.Attributes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Migrator.Program
{
    class Program
    {
        internal class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Pet Pet { get; set; }
        }

        internal class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            // new TypeMigrator(new SQLServerProviderNamespace.SQLServerProvider()).Migrate(new List<Type>() { typeof(Person), typeof(Pet) });
            new TypeMigrator(new SQLServerProviderNamespace.SQLServerProvider()).Migrate(LoadAllBinDirectoryAssemblies());

            //string sql = new TypeMigrator().Migrate(LoadAllBinDirectoryAssemblies());
            //Console.WriteLine(sql); 
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
                    //entityTypes.AddRange(loadedAssembly.GetTypes().Where(t => t.Attributes.co(typeof(Entity))));

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
