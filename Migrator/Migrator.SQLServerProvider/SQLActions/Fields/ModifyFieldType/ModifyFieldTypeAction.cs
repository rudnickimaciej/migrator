using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
using System;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLActions
{
    public class ModifyFieldTypeAction : ISQLAction
    {
        private readonly TFieldModel _field;

        public ModifyFieldTypeAction(TFieldModel field) => this._field = field;

        public List<SQLOperation> GenerateOperations()
        {
            return new List<SQLOperation>()
          {
              new ModifyFieldTypeOperation(_field)
          };
        }
    }
}