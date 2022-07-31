using Migrator.Commons;
using Migrator.ISQLProviderNamespace;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class AddFieldOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CREATE_COLUMN;

        //private void setWrap()
        //{
        //    _wrapSql = $"";
        //}
        public AddFieldOperation(TFieldModel field)
        {
           

             Sql = $"ALTER TABLE {field.EntityName} ADD {field.Name} {field.SqlType}"; //TODO: USE SCHEMA FROM FILE
        }

        public AddFieldOperation(TFieldModel field, string fieldName)
        {
            Sql = $"ALTER TABLE {field.EntityName} ADD {fieldName} {field.SqlType}"; //TODO: USE SCHEMA FROM FILE
        }

        public AddFieldOperation(string table, string fieldName, SQLType sqlType)
        {
            Sql = $"ALTER TABLE {table} ADD {fieldName} {sqlType.ToString()}"; //TODO: USE SCHEMA FROM FILE
        }


    } 
}