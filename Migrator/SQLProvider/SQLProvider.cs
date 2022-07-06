using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Migrator
{
    internal interface ISqlProvider
    {
        SQLPackage Parse(Type type);
    }
    internal class SQLProvider : ISqlProvider
    {
        TModelConverter converter = new TModelConverter();

        public SQLPackage Parse(Type type)
        {
            XmlDocument migration = GetMigrationFromDb(type.Name);
            if (string.IsNullOrEmpty(migration.InnerText))
                return InitTable(TModelConverter.ConvertTypeToTypeModel(type));
                //Pierwsza inicjalizacja
            else
            {
                //Tabela już istnieje, trzeba porównać wersje 
                TModel oldXmlModel = TModelConverter.ConverXmlToTypeModel(new XmlDocument()) ;
            }
            return null;
        }

        internal SQLPackage InitTable(TModel model)
        {
            SQLPackage package = new SQLPackage();
            foreach (TFieldModel field in model.Fields)
                package.Add(MapField(field));
            return package;
        }
        internal List<SQLScript> MapField(TFieldModel field)
        {
            switch (field.Type)
            {
                case FieldType.SIMPLE:
                    return new List<SQLScript>() { new SQLScript(String.Format("{0} {1} NOT NULL", field.Name, field.SqlType), SqlScriptType.ADD_COLUMN) };

                case FieldType.REFERENCE:
                    return new List<SQLScript>() { new SQLScript(String.Format("{0} INT REFERENCES {1}(Id) NOT NULL", field.Name, field.NetType), SqlScriptType.ADD_FOREING_KEY) };

                case FieldType.SIMPLE_LIST:

                    return new List<SQLScript>() { new SQLScript(String.Format("CREATE TABLE {0}_{1} ( ID INT PRIMARY KEY, {2}ID INT FOREIGN KEY REFERENCE{2}", field.EntityName, field.Name + "ID", field.Name), SqlScriptType.CREATE_TABLE) };

                case FieldType.REFERENCE_LIST:
                    return new List<SQLScript>() { new SQLScript(String.Format("ALTER TABLE {0} ADD FOREIGN KEY({1}) REFERENCES {2}(ID)", field.EntityName, field.Name + "ID", field.Name), SqlScriptType.ADD_FOREING_KEY) };

                case FieldType.UNDEFINED:
                    return null;

                default: return null;
            }
        }


        internal void ComputeTypeDifference(TModel newXmlModel, string connectionString)
        {
            

        }

       

        internal XmlDocument GetMigrationFromDb(string entityName)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Migrator"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql.SelectSchemas.Replace("#entityName#", entityName), connection))
                {
                    XmlReader rdr = command.ExecuteXmlReader();
                    XmlDocument xdoc = new XmlDocument();
                    if (rdr.Read())
                        xdoc.Load(rdr);
                    return xdoc;
                }
            }
        }
    }
}
