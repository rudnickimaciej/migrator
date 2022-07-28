using System;
using System.Collections.Generic;

namespace Migrator.ISQLProviderNamespace
{
    public enum SQLOperationType
    {
        CREATE_TABLE = 0,
        DROP_TABLE = 0,
        CREATE_COLUMN = 1,
        DROP_COLUMN = 2,
        ALTER_COLUMN_TYPE = 3,
        DROP_FK = 4,
        ADD_FK =5,
        MIGRATE_COLUMN = 6,
        CHANGE_COLUMN_NAME = 7
    }

    public abstract class SQLOperation
    {
        protected string _sql;
        abstract public SQLOperationType Type { get; }
        public string Execute() => _sql;
        public override string ToString()
        {
            return _sql;
        }
    }

    
}