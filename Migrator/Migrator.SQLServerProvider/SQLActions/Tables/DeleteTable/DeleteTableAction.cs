using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
using System;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLActions
{
    public class DeleteTableAction : ISQLAction
    {
        private readonly TModel _table;
        public DeleteTableAction(TModel table)
        {
            _table = table;
        }

        public List<SQLOperation> GenerateOperations()
        {
            return new List<SQLOperation>()
            {
                new DeleteTableOperation(_table.EntityName)
            };
        }

    }
}