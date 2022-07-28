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
            _sql = $"ALTER TABLE {field.EntityName} ALTER COLUMN {field.Name} {field.SqlType}";
        }

    }
}