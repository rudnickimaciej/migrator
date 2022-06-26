using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class AddTableAction : ISQLAction
    {
        public AddTableAction(XMLModel table)
        {
        }

        public List<SQLOperation> GenerateOperations()
        {
            throw new NotImplementedException();
        }
    }
}