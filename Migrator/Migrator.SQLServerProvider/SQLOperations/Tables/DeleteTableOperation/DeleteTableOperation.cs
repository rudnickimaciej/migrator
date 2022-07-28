using Migrator.ISQLProviderNamespace;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class DeleteTableOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.DROP_TABLE;

        public DeleteTableOperation(string name)
        {
            _sql = $"DROP TABLE {name}"; //TODO: USE SCHEMA FROM FILE
        }

    }
}