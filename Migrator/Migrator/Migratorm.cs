using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

    public class TypeMigrator
    {
        ISQLProvider _sqlProvider;
        private readonly string _projectName = "Migrator";

        public TypeMigrator(ISQLProvider sqlProvider)
        {
            _sqlProvider = sqlProvider;
        }

        public string Migrate(List<Type> types)
        {
            _sqlProvider.CreateConfigurationTables(ConfigurationManager.ConnectionStrings[_projectName].ConnectionString);
            List<TModel> oldSchemas = _sqlProvider.GetSchemasFromDb(ConfigurationManager.ConnectionStrings[_projectName].ConnectionString)
                                                   .Select(doc=>TModelConverter.ConverXmlToTypeModel(doc.xml))
                                                   .ToList();

            List<TModel> newSchemas = types.Select(t => TModelConverter.ConvertTypeToTypeModel(t)).ToList();
            List<TModelPair> schemaPairs = TModelHelper.PairSchemas(oldSchemas, newSchemas);

            IEnumerable<ISQLAction> actions = OperationActionHelper.FlattenActions(schemaPairs.Select(p => _sqlProvider.CreateActions(p)));

            // ISqlProvider sqlProvider = new SQLProvider();
            // List<Node> nodes = types.Select(t => new Node(t)).ToList();
            // List<Node> sortedNoted = new List<Node>();
            // SortEntities(0, nodes, ref sortedNoted);

            //List<SQLPackage> packages = nodes.Select(n => sqlProvider.Parse(n.Type)).ToList();

            // List<SQLScript> scripts = SortByType(FlattenPackages(packages));
            var sortedOperations = OperationActionHelper.SortByType(OperationActionHelper.FlattenOperations(actions.Select(a => a.GenerateOperations())));
            //Merge into FULL SQL
            
            return "Full SQL";
        }

      


        //private static void SortEntities(int i, List<Node> list, ref List<Node> sortedList)
        //{
        //    while (i < list.Count)
        //    {
        //        Node node = list[i];
        //        if (list[i].HasReference())
        //            SortEntities(i + 1, list, ref sortedList);

        //        if (!sortedList.Any(n => n.Type.Equals(node.Type)))
        //            sortedList.Add(list[i]);
        //        //ProcessEntities(++i, ref list, ref sortedList);
        //        i++;
        //    }
        //}
    }
}
