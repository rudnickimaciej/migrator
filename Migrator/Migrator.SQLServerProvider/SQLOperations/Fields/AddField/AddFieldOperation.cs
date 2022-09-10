using Migrator.Commons;
using Migrator.ISQLProviderNamespace;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class AddFieldOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CREATE_COLUMN;

        public AddFieldOperation(TFieldModel field) =>
            setSql(field.EntityName, field.Name, field.SqlType, field.FieldLength);
        

        public AddFieldOperation(TFieldModel field, string fieldName) =>
            setSql(field.EntityName, fieldName, field.SqlType, field.FieldLength);


        public AddFieldOperation(string table, string fieldName, SQLType sqlType, int fieldLength) =>
            setSql(table, fieldName, sqlType, fieldLength);


        private void setSql(string table, string fieldName, SQLType sqlType,int fieldLength)
        {
            Sql = "ALTER TABLE " + table + " ADD " + fieldName + " " + sqlType +
            (fieldLength != -1 ? "(" + fieldLength + ")" : "(1000)");
        }
    } 
}