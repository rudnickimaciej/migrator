using System.Collections.Generic;

namespace Migrator
{
    internal class DeleteFKOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.DROP_FK;

        public DeleteFKOperation(TFieldModel field)
        {
            _sql = $"DECLARE @SQL nvarchar(1000) SET @SQL = 'ALTER {field.EntityName} " +
                   $"DROP CONSTRAINT  (SELECT NAME FROM  sys.foreign_keysWHERE NAME LIKE 'FK__{field.EntityName}__{field.Name}% ')' " +
                   $"EXEC (@SQL)";
        }

    }
}