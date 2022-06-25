using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class CreateTableAction : ISQLAction
    {
        public CreateTableAction(XMLModel table)
        {
        }

        public List<SQLOperation> Execute()
        {
            throw new NotImplementedException();
        }
    }
}