using System.Collections.Generic;

namespace Migrator
{
    internal class DeleteFieldOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.DROP_COLUMN;

        public DeleteFieldOperation(XMLModelField field)
        {
            _sql = $"ALTER TABLE {field.EntityName} DROP COLUMN {field.Name}"; //TODO: USE SCHEMA FROM FILE
        }

    }
}