using System;
using System.Collections.Generic;

namespace Migrator
{
    internal enum SQLOperationType
    {
        CREATE_TABLE = 0,
        CREATE_COLUMN = 1,
        DELETE_COLUMN = 2,
        ALTER_COLUMN_TYPE = 3
    }

    internal interface ISQLOperation
    {
        SQLOperationType OperationType();
        string Execute();

    }
    internal abstract class SQLOperation
    {
        protected Dictionary<string, string> _parameters;
        protected SQLOperationType _operationType;

        public SQLOperation(Dictionary<string, string> parameters) => this._parameters = parameters;

        public abstract string Execute();
    }

    internal class DeleteFieldOperation : SQLOperation
    {
        public DeleteFieldOperation(Dictionary<string, string> parameters) : base(parameters){  }

        public override string Execute()
        {
            return $"ALTER TABLE{_parameters["table"]} DROP COLUMN{_parameters["column"]}"; //TODO: USE SCHEMA FROM FILE
        }
    }
}