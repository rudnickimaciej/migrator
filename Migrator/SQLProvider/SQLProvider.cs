using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Migrator
{
    internal interface ISqlProvider
    {
        List<XmlDoc> GetSchemasFromDb();
        void CreateConfigurationTables();
    }
    internal class SQLProvider : ISqlProvider
    {

        public void CreateConfigurationTables()
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

        public List<XmlDoc> GetSchemasFromDb()
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

     
    }
}
