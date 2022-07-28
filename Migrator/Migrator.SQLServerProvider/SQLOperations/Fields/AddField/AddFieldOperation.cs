using Migrator.Commons;
using Migrator.ISQLProviderNamespace;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class AddFieldOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CREATE_COLUMN;

        public AddFieldOperation(TFieldModel field, )
        {
            _sql = $"ALTER TABLE {field.EntityName} ADD {field.Name} {field.SqlType}"; //TODO: USE SCHEMA FROM FILE
        }

        public AddFieldOperation(TFieldModel field, string fieldName)
        {
            _sql = $"ALTER TABLE {field.EntityName} ADD {fieldName} {field.SqlType}"; //TODO: USE SCHEMA FROM FILE
        }
    } 
}