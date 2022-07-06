using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class DeleteFieldAction : ISQLAction
    {
        private readonly TFieldModel _field;

        public DeleteFieldAction(TFieldModel field) => this._field = field;

        public List<SQLOperation> GenerateOperations()
        {
            throw new NotImplementedException();
        }
    }
}