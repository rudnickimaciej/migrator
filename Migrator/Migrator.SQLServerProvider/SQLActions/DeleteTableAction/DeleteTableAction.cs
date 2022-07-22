using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using System;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLActions
{
    public class DeleteTableAction : ISQLAction
    {
        public DeleteTableAction(TModel table)
        {
        }

        public List<SQLOperation> GenerateOperations()
        {
            throw new NotImplementedException();
        }

    }
}