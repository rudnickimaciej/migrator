using System.Collections.Generic;

namespace Migrator
{
    internal class DeleteFKOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.DROP_FK;

        public DeleteFKOperation(XMLModelField field)
        {
            _sql = $"ALTER TABLE {field.EntityName} DROP COLUMN {field.Name}"; //TODO: USE SCHEMA FROM FILE
        }

    }
}