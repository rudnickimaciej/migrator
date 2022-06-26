using System.Collections.Generic;

namespace Migrator
{
    internal class AddFKOperation: SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.ADD_FK;  
        public AddFKOperation(XMLModelField field)
        {
            _sql = "ADD FOREIGN KEY????"; //TODO: USE SCHEMA FROM FILE
        }

    }
}