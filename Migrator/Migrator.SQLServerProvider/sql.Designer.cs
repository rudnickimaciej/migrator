﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Migrator.SQLServerProviderNamespace
{
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class sql {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal sql() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Migrator.SQLProvider.sql", typeof(sql).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to BEGIN TRANSACTION
        ///
        ///IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N&apos;migrator&apos;)
        ///    EXEC (&apos;CREATE SCHEMA [migrator]&apos;);
        ///
        ///if not exists (select * from sysobjects where name = &apos;SchemaVersion&apos;)
        ///    CREATE TABLE [migrator].[SchemaVersion]
        ///    (
        ///        [VersionId] [int] PRIMARY KEY IDENTITY(100, 1) NOT NULL,
        ///        [Created] [datetime] NOT NULL
        ///    )
        ///
        ///if not exists (select * from sysobjects where name = &apos;Schema&apos;)
        ///    CREATE TABLE [migrator].[Migrations]
        ///    (
        ///        [ID] [int] IDENTITY(1,  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string InitMigratorTables {
            get {
                return ResourceManager.GetString("InitMigratorTables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT EntitySchemaXML FROM [migrator].[Schema] 
        ///WHERE VersionId = (SELECT TOP 1 VersionId FROM [migrator].[SchemaVersion] 
        ///					ORDER BY CREATED DESC).
        /// </summary>
        internal static string SelectSchemas {
            get {
                return ResourceManager.GetString("SelectSchemas", resourceCulture);
            }
        }
    }
}