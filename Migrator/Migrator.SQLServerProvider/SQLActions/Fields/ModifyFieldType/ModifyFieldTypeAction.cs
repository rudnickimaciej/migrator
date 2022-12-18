using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
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
             new AddFieldOperation(_field, $"{_field.Name}_temp"),
             new MigrateColumnOperation(_field),
             new RenameFieldOperation(_field.EntityName, $"{_field.Name}_temp",_field.Name),
             new DeleteFieldOperation(_field)
          };
        }
    }
}