using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class AddFKFieldOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CREATE_COLUMN;

        public AddFKFieldOperation(TFieldModel field) 
        {  
            _sql = $"ALTER TABLE {field.EntityName} ADD {field.Name}Id {field.SqlType}"; //TODO: USE SCHEMA FROM FILE
        }
    }
}