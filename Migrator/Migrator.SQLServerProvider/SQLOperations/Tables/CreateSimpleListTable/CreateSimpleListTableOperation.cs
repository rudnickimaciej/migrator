using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class CreateSimpleListTableOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CREATE_TABLE;

        public CreateSimpleListTableOperation(TModel entity)
        {
            Sql = $"CREATE TABLE {entity.EntityName} (ID INT PRIMARY KEY)"; //TODO: USE SCHEMA FROM FILE
        }

    }
}