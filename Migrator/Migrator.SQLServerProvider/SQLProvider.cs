using Migrator.ISQLProviderNamespace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Migrator.Commons;
using Migrator.SQLServerProviderNamespace.SQLActions;
using Migrator.Commons.Extensions;
using Migrator.Core;

namespace Migrator.SQLServerProviderNamespace
{
    public class SQLServerProvider : ISQLProvider
    {
        public SQLServerProvider()
        {
        }

        public IEnumerable<ISQLAction> CreateActions(TModelPair pair)
        {
            if (pair.SchemaPair.Item1 == null)
                return new List<ISQLAction>() { new AddTableAction(pair.SchemaPair.Item2) };

            if (pair.SchemaPair.Item2 == null)
                return new List<ISQLAction>() { new DeleteTableAction(pair.SchemaPair.Item1) };

            return ProcessModel(pair); //TOODO ZMIENIC NAZWER METODY
        }

        private  IEnumerable<ISQLAction> ProcessModel(TModelPair pair)
        {

            List<TFieldModelPair> fieldsPairs = TModelHelper.PairFields(pair);


            foreach (TFieldModelPair fieldPair in fieldsPairs)
            {
                TFieldModel oldField = fieldPair.FieldPair.Item1;
                TFieldModel newField = fieldPair.FieldPair.Item2;

                if (oldField == null)
                    yield return new AddFieldAction(newField);

                if (newField == null)
                    yield return new DeleteFieldAction(oldField);

                if (oldField != null && newField != null && (oldField.Type != newField.Type || oldField.NetType != newField.NetType))
                    yield return new ModifyFieldTypeAction(newField);
            }
        }
        public void CreateConfigurationTables(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sqlEx = sql.InitMigratorTables.Replace('\t', ' ').Replace('\n', ' ').Replace('\r', ' ');
                Console.WriteLine(sqlEx);
                using (SqlCommand command = new SqlCommand(sqlEx, connection))
                {
                    command.ExecuteNonQuery();

                }
            }
        }

        public void ExecuteScript(string sql, List<XmlDoc> xmls)
        {
            throw new NotImplementedException();
        }

        public List<XmlDoc> GetSchemasFromDb(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sqlEx = sql.SelectSchemas.Replace('\t', ' ').Replace('\n', ' ').Replace('\r', ' ');
                Console.WriteLine(sqlEx);
                using (IDataReader reader = new SqlCommand(sqlEx, connection).ExecuteReader())
                {
                    return reader.Select(r => new XmlDoc(r["EntitySchemaXML"] is DBNull ? null : r["EntitySchemaXML"].ToString())
                  ).ToList();
                }
            }
        }

        public void ExecuteScript(string sql)
        {
            throw new NotImplementedException();
        }
    }
}