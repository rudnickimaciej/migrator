using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
using System;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLActions
{
    public class AddTableAction : ISQLAction
    {
        private readonly TModel _table;
        public AddTableAction(TModel table) => this._table = table;

        public List<SQLOperation> GenerateOperations()
        {
            List <SQLOperation> list = new List<SQLOperation>() { new CreateTableOperation(_table.EntityName) };
            _table.Fields.ForEach(field=>list.AddRange(new AddFieldAction(field).GenerateOperations()));
            return list;
        }
    }
}