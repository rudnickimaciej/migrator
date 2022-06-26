using System;
using System.Collections.Generic;

namespace Migrator
{
    internal enum SQLOperationType
    {
        CREATE_TABLE = 0,
        DROP_TABLE = 0,
        CREATE_COLUMN = 1,
        DROP_COLUMN = 2,
        ALTER_COLUMN_TYPE = 3,
        DROP_FK = 4,
        ADD_FK =5
    }

    internal abstract class SQLOperation
    {
        protected string _sql;
        abstract public SQLOperationType Type { get; }
        public string Execute() => _sql;
    }
}