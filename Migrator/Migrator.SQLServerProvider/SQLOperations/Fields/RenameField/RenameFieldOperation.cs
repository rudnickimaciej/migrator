using Migrator.ISQLProviderNamespace;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    internal class RenameFieldOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.CHANGE_COLUMN_NAME;

        public RenameFieldOperation(string table, string oldName, string newName)
        {
            Sql = $"EXEC sp_rename '{table}.{oldName}','{newName}', 'COLUMN'";
        }
    }
}
