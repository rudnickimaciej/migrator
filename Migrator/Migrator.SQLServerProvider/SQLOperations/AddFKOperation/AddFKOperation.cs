using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class AddFKOperation: SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.ADD_FK;  
        public AddFKOperation(TFieldModel field)
        {
            _sql = $"ALTER TABLE {field.EntityName} ADD FOREIGN KEY ({field.NetType}) REFERENCES {field.NetType}(ID)"; //TODO: USE SCHEMA FROM FILE
        }

    }
}