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
            if (_field.Type.Equals(FieldType.SIMPLE))
                return new List<SQLOperation>()
              {
                  new DeleteFKOperation(_field),
                  new DeleteFieldOperation(_field)
              };
          
            if (_field.Type.Equals(FieldType.REFERENCE))
                return new List<SQLOperation>()
              {
                  new DeleteFKOperation(_field),
                  new DeleteFieldOperation(_field)
              };
            if (_field.Type.Equals(FieldType.SIMPLE_LIST))
                return new List<SQLOperation>()
              {
                  new DeleteTableOperation($"{_field.EntityName}_{_field.Name}")
              };
            if (_field.Type.Equals(FieldType.REFERENCE_LIST))
                return new List<SQLOperation>()
              {
                  new DeleteTableOperation($"{_field.EntityName}_{_field.Name}")
              };

            return null;

        }
    }
}