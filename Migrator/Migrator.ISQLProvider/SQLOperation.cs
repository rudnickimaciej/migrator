using System;
using System.Collections.Generic;

namespace Migrator.ISQLProviderNamespace
{
    public enum SQLOperationType
    {
        CREATE_TABLE,
        DROP_TABLE,
        CREATE_COLUMN,
        DROP_FK,
        DROP_COLUMN,
        ALTER_COLUMN_TYPE,
        ADD_FK,
        MIGRATE_COLUMN,
        CHANGE_COLUMN_NAME
    }

    public abstract class SQLOperation
    {
        public string Sql { get; protected set; }

       // protected string _wrapSql;

        abstract public SQLOperationType Type { get; }
        public string Execute() => Sql;
        public override string ToString()
        {
            return Sql;
        }

        //protected string WrapOperation(string sql)
        //{
        //    return string.Format(_wrapSql, sql);
        //}
    }

    
}