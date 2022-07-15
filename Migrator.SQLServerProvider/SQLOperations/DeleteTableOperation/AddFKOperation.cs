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
            _sql = "ADD FOREIGN KEY????"; //TODO: USE SCHEMA FROM FILE
        }

    }
}