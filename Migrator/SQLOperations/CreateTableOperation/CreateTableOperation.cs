using System.Collections.Generic;

namespace Migrator
{
    internal class CreateTableOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CREATE_TABLE;

        public CreateTableOperation(string name)
        {
            _sql = $"CREATE TABLE {name} (ID INT PRIMARY KEY)"; //TODO: USE SCHEMA FROM FILE
        }

    }
}