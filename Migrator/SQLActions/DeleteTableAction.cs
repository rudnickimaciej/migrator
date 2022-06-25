using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class DeleteTableAction : ISQLAction
    {
        public DeleteTableAction(XMLModel table)
        {
        }

        public List<SQLOperation> Execute()
        {
            throw new NotImplementedException();
        }
    }
}