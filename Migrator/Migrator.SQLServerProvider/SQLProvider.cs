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
using System.Text;
using Migrator.Commons.Logger;

namespace Migrator.SQLServerProviderNamespace
{
    public class SQLServerProvider : ISQLProvider
    {
        private readonly ILogger _logger;

        public SQLServerProvider(ILogger logger)
        {
            _logger = logger;
        }
        public SQLServerProvider()
        {
            
        }

        public IEnumerable<ISQLAction> CreateActions(TModelPair pair)
        {
            if (pair.SchemaPair.Item1 == null)
                return new List<ISQLAction>() { new AddTableAction(pair.SchemaPair.Item2) };

            if (pair.SchemaPair.Item2 == null)
                return new List<ISQLAction>() { new DeleteTableAction(pair.SchemaPair.Item1) };

            return ProcessModel(pair); //TODO ZMIENIC NAZWER METODY
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

                if (oldField != null && newField != null && (
                    oldField.Type != newField.Type ||
                    oldField.NetType != newField.NetType ||
                    oldField.FieldLength != newField.FieldLength))
                    yield return new ModifyFieldTypeAction(newField);
            }
        }

        public List<TModel> GetCurrentSchemas(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var sqlEx = @sql.SelectSchemas.Clean();
                _logger.Log(sqlEx, ConsoleColor.DarkCyan, ConsoleColor.White);
                using (IDataReader reader = new SqlCommand(sqlEx, connection).ExecuteReader())
                {
                    return reader.Select(r => new XmlDoc(r["EntitySchemaXML"] is DBNull ? null : r["EntitySchemaXML"].ToString())
                  ).Select(doc => TModelConverter.ConverXmlToTModel(doc.xml)).ToList();  
                }
            }
        }

        public void ExecuteScript(string connectionString, string sqls, IEnumerable<TModel> newSchemas)
        {
            StringBuilder migrationInserts = new StringBuilder();
            newSchemas
                .ToList()
                .ForEach(s => migrationInserts.AppendLine($"INSERT INTO migrator.Migrations SELECT @Version, '{s.EntityName}', " +
                                                          $"'{TModelConverter.ConverTypeModelToXML(s).InnerXml}', GETDATE()"));

            string formattedSql = string.Format(sql.Transaction.Clean(),
                sql.InitMigratorTables.Clean(),
                sqls,
                sql.IncrementSchemaVersion.Clean(),
                sql.DeclareVersionVariable.Clean(),
                migrationInserts.ToString());

            _logger.Log(formattedSql, ConsoleColor.Green, ConsoleColor.Black);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int i = new SqlCommand(formattedSql, connection).ExecuteNonQuery();
            }
        }
    }
}