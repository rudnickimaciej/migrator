using Migrator.Attributes;
using Migrator.ISQLProviderNamespace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Migrator
{
    public class Application : IApplication
    {
        private readonly ITypeMigrator _migrator;

        public Application( ITypeMigrator migrator)
        {
            _migrator = migrator;
        }

        public void Run(string path) =>
            _migrator.Migrate(LoadAllBinDirectoryAssemblies(path));
       
        private static List<Type> LoadAllBinDirectoryAssemblies(string path)
        {
            List<Type> entityTypes = new List<Type>();
            foreach (string dll in Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    Assembly loadedAssembly = Assembly.LoadFile(dll);
                    entityTypes.AddRange(loadedAssembly.GetTypes().Where(t => t.IsDefined(typeof(Entity))));

                }
                catch (FileLoadException loadEx)
                {
                    throw loadEx;
                }
                catch (BadImageFormatException imgEx)
                {
                    throw imgEx;
                }

            }
            foreach (string dll in Directory.GetFiles(path, "*.exe", SearchOption.AllDirectories))
            {
                try
                {
                    Assembly loadedAssembly = Assembly.LoadFile(dll);
                    entityTypes.AddRange(loadedAssembly.GetTypes().Where(t => t.IsDefined(typeof(Entity))));

                }
                catch (FileLoadException loadEx)
                {
                    throw loadEx;
                }
                catch (BadImageFormatException imgEx)
                {
                    throw imgEx;
                }

            }

            return entityTypes;
        }
    }


}
