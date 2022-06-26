using System.Collections.Generic;

namespace Migrator
{
    internal class AddFieldOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CREATE_COLUMN;

        public AddFieldOperation(XMLModelField field) 
        {  
            _sql = $"ALTER TABLE{field.EntityName} ADD {field.Name} {field.SqlType}"; //TODO: USE SCHEMA FROM FILE
        }
    }
}