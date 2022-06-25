using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class DeleteFieldAction : ISQLAction
    {
        private readonly XMLModelField _field;

        public DeleteFieldAction(XMLModelField field) => this._field = field;

        public List<SQLOperation> GenerateOperations()
        {
            throw new NotImplementedException();
        }
    }
}