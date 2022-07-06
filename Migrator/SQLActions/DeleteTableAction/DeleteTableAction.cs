using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class DeleteTableAction : ISQLAction
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