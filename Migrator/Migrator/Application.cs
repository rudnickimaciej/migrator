﻿using Migrator.Attributes;
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

        public void Run() =>
            _migrator.Migrate(LoadAllBinDirectoryAssemblies());
       
        private static List<Type> LoadAllBinDirectoryAssemblies()
        {
            List<Type> entityTypes = new List<Type>();
            foreach (string dll in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory), "*.dll", SearchOption.AllDirectories))
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