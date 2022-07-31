using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    internal class DeleteFieldOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.DROP_COLUMN;

        public DeleteFieldOperation(TFieldModel field)
        {
            Sql = $"ALTER TABLE {field.EntityName} DROP COLUMN {field.Name}"; //TODO: USE SCHEMA FROM FILE
        }

        public DeleteFieldOperation(string table, string fieldName)
        {
            Sql = $"ALTER TABLE {table} DROP COLUMN {fieldName}"; 
        }
    }
}