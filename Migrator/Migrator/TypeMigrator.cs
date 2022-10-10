using Migrator.Commons;
using Migrator.Commons.Logger;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;

[assembly: InternalsVisibleToAttribute("Migrator.Program")]

namespace Migrator
{

    internal class SQLPackage
    {
        public SQLPackage(params SQLScript[] scripts)
        {
            this.Scripts = scripts.ToList();
        }

        public List<SQLScript> Scripts { get; set; }
        public void Add(SQLScript script) => this.Scripts.Add(script);
        public void Add(List<SQLScript> scripts) => this.Scripts.AddRange(scripts);

    }
    internal class SQLScript
    {
        public SQLScript()
        {
        }

        public SQLScript(string sql, SqlScriptType sqlScriptType)
        {
            Sql = sql;
            SqlScriptType = sqlScriptType;
        }

        public string Sql { get; set; }
        public SqlScriptType SqlScriptType { get; set; }

    }

    internal enum SqlScriptType
    {
        CREATE_TABLE,
        ALTER_COLUMN_NAME,
        DELETE_COLUMN,
        ADD_COLUMN,
        ADD_FOREING_KEY,
        CREATE_RELATIONSHIP_TABLE,
        ALTER_COLUMN_TYPE,
        DELETE_TABLE
    }
    public interface ITypeMigrator
    {
         void Migrate(List<Type> types);
    }

    public class TypeMigrator : ITypeMigrator
    {

        private readonly ISQLProvider _sqlProvider;
        private readonly ILogger _logger;

        public TypeMigrator(ISQLProvider sqlProvider, ILogger logger)
        {
            _sqlProvider = sqlProvider;
            _logger = logger;
        }

        public void Migrate(List<Type> types)
        {
            string connString = ConfigurationManager.ConnectionStrings["Migrator"].ConnectionString;
            IEnumerable<TModel> oldSchemas = _sqlProvider.GetCurrentSchemas(connString);
   

            List<TModel> newSchemas = types.Select(t => TModelConverter.ConvertTypeToTypeModel(t)).ToList();
            List<TModelPair> schemaPairs = TModelHelper.PairSchemas(oldSchemas.ToList(), newSchemas);

            IEnumerable<ISQLAction> actions = OperationActionHelper.FlattenActions(schemaPairs.Select(p => _sqlProvider.CreateActions(p)));

            var operations = actions.Select(a => a.GenerateOperations());
            var flattenOperations = OperationActionHelper.FlattenOperations(operations);
            var filteredOperations = OperationActionHelper.RemoveDuplicates(flattenOperations);
            var sortedOperations = OperationActionHelper.SortByType(filteredOperations);
          
            string sql = OperationActionHelper.Merge(sortedOperations);
            _sqlProvider.ExecuteScript(connString, sql, newSchemas);
        }
    }
}
