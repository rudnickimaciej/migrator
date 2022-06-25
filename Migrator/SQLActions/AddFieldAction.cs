using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class AddFieldAction : ISQLAction
    {
        public AddFieldAction(XMLModelField field)
        {
        }

        public List<SQLOperation> Execute()
        {
            throw new NotImplementedException();
        }
    }
}