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
        internal string Migrate(List<Type> types)
        {
            CreateConfigurationTables();
            List<XMLModel> oldSchemas =  GetSchemasFromDb().Select(doc=>XMLModelConverter.ConverXmlToXMLModel(doc.xml)).ToList();
            List<XMLModel> newSchemas = types.Select(t => XMLModelConverter.ConvertTypeToXMLModel(t)).ToList();
            List<XmlModelPair> schemaPairs = PairSchemas(oldSchemas, newSchemas);
            List<ISQLOperation>


            ISqlProvider sqlProvider = new SQLProvider();
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
        internal static List<XmlModelPair> PairSchemas(List<XMLModel> first, List<XMLModel> second)
        {
            var pairs = new List<Tuple<XMLModel, XMLModel>>();

            pairs.AddRange(first.Intersect(second)
                .Select(match => Tuple.Create(match, match)));

            pairs.AddRange(first.Except(second)
                .Select(inFirst => Tuple.Create(inFirst, (XMLModel)null)));

            pairs.AddRange(second.Except(first)
                .Select(inSecond => Tuple.Create((XMLModel)null, inSecond)));

            return pairs.Select(p => new XmlModelPair(p.Item1, p.Item2)).ToList();
        }

        #region pair functions

        

        internal static List<XmlModelPair> PairSchemas2(List<XMLModel> oldSchemas, List<XMLModel> newSchemas)
        {
            List<XmlModelPair> pairs = new List<XmlModelPair>();
            List<XMLModel> oldSchemasbuffor = new List<XMLModel>(oldSchemas);
            List<XMLModel> newSchemasbuffor = new List<XMLModel>(newSchemas);

            foreach (XMLModel schema in oldSchemas)
            {
                foreach(XMLModel schema2 in newSchemas)
                {
                    if(schema.EntityName == schema2.EntityName)
                    {
                        pairs.Add(new XmlModelPair(schema, schema2));
                        oldSchemasbuffor.Remove(oldSchemasbuffor.Where(s => s.EntityName == schema.EntityName).FirstOrDefault());
                        newSchemasbuffor.Remove(newSchemasbuffor.Where(s => s.EntityName == schema.EntityName).FirstOrDefault());
                    }
                }
            }

            foreach(XMLModel schema in oldSchemasbuffor)
            {
                pairs.Add(new XmlModelPair(oldSchema: schema, newSchema: null));
            }

            foreach (XMLModel schema in newSchemasbuffor)
            {
                pairs.Add(new XmlModelPair(oldSchema: null, newSchema: schema));
            }

            return pairs;
        }

        public static List<XmlModelPair> PairSchemas5(List<XMLModel> oldSchemas, List<XMLModel> newSchemas)
        {
            var result = (from a in oldSchemas
                          join b in newSchemas.Select(x => x) on a.EntityName equals b.EntityName into ab
                          from b in ab.DefaultIfEmpty()
                          select new XmlModelPair(a, b)
                   ).ToList();

             result.Concat(newSchemas.Where(x => !result.Any(y => y.SchemaPair?.Item2?.EntityName == x.EntityName))
                       .Select(x => new XmlModelPair(null, x)))
                .ToList();

            return result.Concat(oldSchemas.Where(x => !result.Any(y => y.SchemaPair?.Item1?.EntityName == x.EntityName))
                      .Select(x => new XmlModelPair(x, null)))
               .ToList() ;
        }

     
        public static List<Tuple<int?, int?>> PairInts(List<int> listOne, List<int> listTwo)
        {
            var result = (from a in listOne
                          join b in listTwo.Select(x => (int?)x) on a equals b into ab
                          from b in ab.DefaultIfEmpty()
                          select Tuple.Create<int?, int?>(a, b)
                   ).ToList();

            return result
                .Concat(listTwo.Where(x => !result.Any(y => y.Item1 == x))
                       .Select(x => Tuple.Create<int?, int?>(null, x)))
                .ToList();
        }

        public static List<Tuple<XMLModel, XMLModel>> PairXmlModels3(List<XMLModel> listOne, List<XMLModel> listTwo)
        {
            var result = (from a in listOne
                          join b in listTwo.Select(x => x) on a equals b into ab
                          from b in ab.DefaultIfEmpty()
                          select Tuple.Create<XMLModel, XMLModel>(a, b)
                   ).ToList();

            return result
                .Concat(listTwo.Where(x => !result.Any(y => y.Item1.EntityName == x.EntityName))
                       .Select(x => Tuple.Create<XMLModel, XMLModel>(null, x)))
                .ToList();
        }
        internal static List<Tuple<int?, int?>> PairInts2(List<int> firstList, List<int> secondList)
        {
            List<Tuple<int?, int?>> pairs = new List<Tuple<int?, int?>>();
            List<int> firstListBackup = new List<int>(firstList);
            List<int> secondListBackup = new List<int>(secondList);

            foreach (int val in firstList)
            {
                foreach (int val2 in secondList)
                {
                    if (val == val2)
                    {
                        pairs.Add(new Tuple<int?, int?>(val, val2));
                        firstListBackup.Remove(firstListBackup.Where(v => v == val).FirstOrDefault());
                        secondListBackup.Remove(secondListBackup.Where(v => v == val2).FirstOrDefault());
                    }
                }
            }

            foreach (int val in firstListBackup)
            {
                pairs.Add(new Tuple<int?, int?>(val, null));
            }

            foreach (int val in secondListBackup)
            {
                pairs.Add(new Tuple<int?, int?>(null, val));
            }

            return pairs;
        }
        #endregion

        internal List<XmlDoc> GetSchemasFromDb()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Migrator"].ConnectionString))
            {
                connection.Open();

                using (IDataReader reader = new SqlCommand(sql.SelectSchemas.Replace('\t', ' ').Replace('\n', ' ').Replace('\r', ' '), connection).ExecuteReader())
                {
                    return reader.Select(r => new XmlDoc(r["EntitySchemaXML"] is DBNull ? null : r["EntitySchemaXML"].ToString())
                  ).ToList();
                }            
            }
        }

        private void CreateConfigurationTables()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Migrator"].ConnectionString))
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
