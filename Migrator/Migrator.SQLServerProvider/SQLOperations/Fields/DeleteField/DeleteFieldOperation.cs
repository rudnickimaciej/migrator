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
            _sql = $"ALTER TABLE {field.EntityName} DROP COLUMN {field.Name}"; //TODO: USE SCHEMA FROM FILE
        }

    }
}