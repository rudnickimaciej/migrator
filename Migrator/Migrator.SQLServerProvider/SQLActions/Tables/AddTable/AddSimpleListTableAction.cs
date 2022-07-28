using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
using System;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLActions
{
    public class AddSimpleListTableAction : ISQLAction
    {
        private readonly TFieldModel _field;
        public AddSimpleListTableAction(TFieldModel field) => this._field = field;

        public List<SQLOperation> GenerateOperations()
        {
            List <SQLOperation> list = new List<SQLOperation>() { new CreateTableOperation($"{_field.EntityName }_{ _field.Name }")};
            list.AddRange(
                    new AddFieldAction(new TFieldModel() { EntityName = _field.EntityName, Name= $"{_field.EntityName}Id", SqlType = _field.SqlType, Type = FieldType.REFERENCE})
                 .GenerateOperations());
            return list;
        }
    }
}