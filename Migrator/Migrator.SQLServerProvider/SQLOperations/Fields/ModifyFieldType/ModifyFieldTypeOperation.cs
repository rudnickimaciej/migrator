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
            bool fieldLength = field.FieldLength != -1;
            bool fieldRequired = field.IsRequired;

            Sql = "ALTER TABLE " + field.EntityName +
                  " ALTER COLUMN " + field.Name + " " + field.SqlType + (fieldLength ? "(" + field.FieldLength + ")" : " ") +
                  " " + (fieldRequired ? "NOT NULL" : " ");
        }

    }
}