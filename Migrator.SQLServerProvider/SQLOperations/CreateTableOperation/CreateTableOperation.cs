using Migrator.ISQLProviderNamespace;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class CreateTableOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CREATE_TABLE;

        public CreateTableOperation(string name)
        {
            _sql = $"CREATE TABLE {name} (ID INT PRIMARY KEY)"; //TODO: USE SCHEMA FROM FILE
        }

    }
}