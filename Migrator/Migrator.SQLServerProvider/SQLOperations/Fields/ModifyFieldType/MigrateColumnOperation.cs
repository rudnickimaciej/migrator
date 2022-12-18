using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    internal class MigrateColumnOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.ALTER_COLUMN_TYPE;

        public MigrateColumnOperation(TFieldModel field)
        {
            Sql = $"EXEC( 'UPDATE {field.EntityName} SET {field.Name}_temp = try_convert({field.SqlType}, {field.Name})')";
        }
    }
}
