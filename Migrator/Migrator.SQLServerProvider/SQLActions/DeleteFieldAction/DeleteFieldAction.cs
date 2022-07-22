using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLActions
{
    public class DeleteFieldAction : ISQLAction
    {
        private readonly TFieldModel _field;

        public DeleteFieldAction(TFieldModel field) => this._field = field;

        public List<SQLOperation> GenerateOperations()
        {
            return new List<SQLOperation>()
          {
              new DeleteFKOperation(_field),
              new DeleteFieldOperation(_field)
          };
        }
    }
}