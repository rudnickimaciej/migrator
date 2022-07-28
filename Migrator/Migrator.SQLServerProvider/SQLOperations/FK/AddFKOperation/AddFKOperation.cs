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
            if(field.Type==FieldType.SIMPLE_LIST || field.Type == FieldType.REFERENCE_LIST)
                _sql = $"ALTER TABLE {field.EntityName} ADD FOREIGN KEY ({field.Name}) REFERENCES {field.EntityName}_{field.Name}(ID)"; //TODO: USE SCHEMA FROM FILE
            else
                _sql = $"ALTER TABLE {field.EntityName} ADD FOREIGN KEY ({field.Name}) REFERENCES {field.Name}(ID)"; //TODO: USE SCHEMA FROM FILE
        }

    }
}