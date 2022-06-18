using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Migrator.Core;

[assembly: InternalsVisibleToAttribute("Migrator.Tests")]
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

    internal class Migratorm
    {
        internal string Migrate(string connectionString, List<Type> types)
        {
            ISqlProvider sqlProvider = new SQLProvider(connectionString);
            List<Node> nodes = types.Select(t => new Node(t)).ToList();
            List<Node> sortedNoted = new List<Node>();
            // SortEntities(0, nodes, ref sortedNoted);

            List<SQLPackage> packages = nodes.Select(n => sqlProvider.Parse(n.Type)).ToList();

            List<SQLScript> scripts = SortByType(FlattenPackages(packages));

            return "Full SQL";
        }
        internal static string ToSQL(List<SQLScript> scripts)
        {
            StringBuilder sb = new StringBuilder();
            scripts.ForEach(s => sb.AppendLine(s.Sql + "GO;"));
            sb.Replace("\r\n", " ");
            return sb.ToString();
        }
        internal static List<SQLScript> FlattenPackages(List<SQLPackage> packages)
        {
            List<SQLScript> flat = new List<SQLScript>();

            foreach (var package in packages)
                foreach (var script in package.Scripts)
                    flat.Add(script);
            return flat;
        }
        internal static List<SQLScript> SortByType(List<SQLScript> list) => list.OrderBy(c => c.SqlScriptType).ToList();


        internal static void SortEntities(int i, List<Node> list, ref List<Node> sortedList)
        {
            while (i < list.Count)
            {
                Node node = list[i];
                if (list[i].HasReference())
                    SortEntities(i + 1, list, ref sortedList);

                if (!sortedList.Any(n => n.Type.Equals(node.Type)))
                    sortedList.Add(list[i]);
                //ProcessEntities(++i, ref list, ref sortedList);
                i++;
            }
        }

        internal void GetSchemasFromDb()
        {


            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Migrator"].ConnectionString))
            {
                connection.Open();


                using (SqlCommand command = new SqlCommand(sql.SelectSchemas.Replace('\t', ' ').Replace('\n', ' ').Replace('\r', ' '), connection))
                {
                    command.ExecuteNonQuery();

                }
            }
        }

        private void CreateConfigurationTables(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                using (SqlCommand command = new SqlCommand(sql.InitMigratorTables.Replace('\t', ' ').Replace('\n', ' ').Replace('\r', ' '), connection))
                {
                    command.ExecuteNonQuery();

                }
            }
        }
    }
}
