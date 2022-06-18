using Migrator.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Migrator.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            string sql = new Migratorm().Migrate(ConfigurationManager.ConnectionStrings["Migrator"].ConnectionString, LoadAllBinDirectoryAssemblies());
            Console.WriteLine(sql);
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

            var attributes = assembly.GetCustomAttributes(typeof(Entity2), false);

            if (attributes.Length > 0)
                return true;
            

            return false;
        }
    }
}
