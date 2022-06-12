using Migrator.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Migrator.Core
{
    internal interface ISqlProvider
    {
        SQLPackage Parse(Type type);
    }
    internal class SQLProvider : ISqlProvider
    {
        XMLModelConverter converter = new XMLModelConverter();
        private readonly string _connectionString;
        public SQLPackage Parse(Type type)
        {
            XmlDocument migration = GetMigrationFromDb(type.Name);
            if (migration == null)
                return InitTable(XMLModelConverter.ConvertTypeToXMLModel(type));
            //Pierwsza inicjalizacja

            else
            {
                //Tabela już istnieje, trzeba porównać wersje 
                XMLModel oldXmlModel = XMLModelConverter.ConverXmlToXMLModel(new XmlDocument()) ;
            }
            return null;
        }

        internal SQLPackage InitTable(XMLModel model)
        {
            SQLPackage package = new SQLPackage();
            foreach (XMLModelField field in model.Fields)
                package.Add(MapField(field));
            return package;
        }
        internal SQLScript MapField(XMLModelField field)
        {
            switch (field.fieldType)
            {
                case FieldType.SIMPLE:
                    return new SQLScript(String.Format("{0} {1} NOT NULL", field.fieldName, field.sqlType), SqlScriptType.ADD_COLUMN);

                case FieldType.REFERENCE:
                    return new SQLScript(String.Format("{0} INT REFERENCES {1}(Id) NOT NULL", field.fieldName, field.netType), SqlScriptType.ADD_FOREING_KEY);

                case FieldType.SIMPLE_LIST:
                case FieldType.REFERENCE_LIST:
                    return new SQLScript(String.Format("{0} INT REFERENCES {1}(Id) NOT NULL", field.fieldName, field.EntityName + "_" + field.fieldName), SqlScriptType.ADD_FOREING_KEY);

                case FieldType.UNDEFINED:
                    return null;

                default: return null;
            }
        }
        internal void ComputeTypeDifference(XMLModel newXmlModel, string connectionString)
        {
            

        }

       

        internal XmlDocument GetMigrationFromDb(string entityName)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Migrator.sql.SelectFromMigration.Replace("#entityName#", entityName), connection))
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
