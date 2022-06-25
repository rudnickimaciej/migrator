using System;
using System.Collections.Generic;

namespace Migrator
{
    internal enum SQLOperationType
    {
        CREATE_TABLE = 0,
        CREATE_COLUMN = 1
    }


    internal class SQLOperation
    {
        private string sql;
        private SQLOperationType OperationType { get; }
    }
}