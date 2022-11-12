using Migrator.Commons;
using Migrator.Commons.Helpers;
using Migrator.ISQLProviderNamespace;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class DeleteFKOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.DROP_FK;

        public DeleteFKOperation(TFieldModel field)
        {
            string sqlVarName = $"@SQL_{RandomHelper.GetRandomString()}";
            Sql = $"DECLARE {sqlVarName} nvarchar(1000) SET {sqlVarName} = 'ALTER TABLE {field.EntityName} " +
                   $"DROP CONSTRAINT ' + (SELECT NAME FROM  sys.foreign_keys WHERE NAME LIKE 'FK__{field.EntityName}__{field.Name}%') " +
                   $"EXEC ({sqlVarName})";
        }

    }
}