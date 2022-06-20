using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal enum SQLOperationType
    {
        CREATE_TABLE = 0,
        CREATE_COLUMN = 1
    }
    internal interface ISQLAction
    {
        List<SQLOperation> Execute();
    }
    internal class SQLOperation
    {

        SQLOperationType OperationType { get; }
        string sql;

    }

    internal class CreateTableAction : ISQLAction
    {
        public CreateTableAction(XMLModel table)
        {

        }


        public List<SQLOperation> Execute()
        {
            throw new NotImplementedException();
        }
    }

    internal class DeleteTableAction : ISQLAction
    {
        public DeleteTableAction(XMLModel table)
        {

        }

        public List<SQLOperation> Execute()
        {
            throw new NotImplementedException();
        }
    }
}
