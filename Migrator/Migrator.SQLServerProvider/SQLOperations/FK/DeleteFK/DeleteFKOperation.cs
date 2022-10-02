using Migrator.Commons;
using Migrator.ISQLProviderNamespace;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    public class DeleteFKOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.DROP_FK;

        public DeleteFKOperation(TFieldModel field)
        {
            Sql = $"DECLARE @SQL nvarchar(1000) SET @SQL = 'ALTER TABLE {field.EntityName} " +
                   $"DROP CONSTRAINT' (SELECT NAME FROM  sys.foreign_keys WHERE NAME LIKE 'FK__{field.EntityName}__{field.Name}%') " +
                   $"EXEC (@SQL)";
        }

    }
}