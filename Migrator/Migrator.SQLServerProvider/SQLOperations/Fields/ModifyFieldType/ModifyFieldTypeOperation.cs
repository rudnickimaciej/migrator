using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    internal class ModifyFieldTypeOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.ALTER_COLUMN_TYPE;

        public ModifyFieldTypeOperation(TFieldModel field)
        {
            bool fieldLengthFlag = field.FieldLength != -1;

            Sql = "ALTER TABLE " + field.EntityName +
                  " ALTER COLUMN " + field.Name + " " + field.SqlType + (fieldLengthFlag ? "(" + field.FieldLength + ")" : "");
        }

        private int getLength(int fieldLength)
        {
            if (fieldLength == -1)
                return Consts.VARCHAR_DEFAULT_LENGTH;
            return fieldLength;
        }
    }
}