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

            bool fieldLengthFlag = fieldLength != -1;

            Sql = "ALTER TABLE " + table + " ADD " + fieldName + " " + sqlType +
            (fieldLengthFlag ? "(" + fieldLength + ")" : "") + " GO";
        }

        private int getLength(int fieldLength)
        {
            if (fieldLength == -1)
                return Consts.VARCHAR_DEFAULT_LENGTH;
            return fieldLength;
        }
    } 
}