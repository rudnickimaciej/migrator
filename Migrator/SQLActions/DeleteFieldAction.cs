using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class DeleteFieldAction : ISQLAction
    {
        public DeleteFieldAction(XMLModelField field)
        {
        }

        public List<SQLOperation> Execute()
        {
            throw new NotImplementedException();
        }
    }
}