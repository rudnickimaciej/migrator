using System;
using System.Collections.Generic;

namespace Migrator
{
    internal class AddFieldAction : ISQLAction
    {
        private readonly XMLModelField _field;
        public AddFieldAction(XMLModelField field) => _field = field;

        public List<SQLOperation> GenerateOperations()
        {
            if (_field.fieldType.Equals(FieldType.SIMPLE))
                return new List<SQLOperation>() { new SQLOperation($"ALTER TABLE {_field.EntityName} ADD {_field.fieldName} {_field.sqlType}", SQLOperationType.CREATE_COLUMN)};

            if (_field.fieldType.Equals(FieldType.SIMPLE_LIST))
                return new List<SQLOperation>() {
                    new SQLOperation($"CREATE TABLE {_field.EntityName}_{_field.fieldName} (ID INT PRIMARY KEY, {_field.EntityName}ID INT, Val {_field.sqlType}", SQLOperationType.CREATE_TABLE),
                    new SQLOperation($"ALTER TABLE {_field.EntityName} ADD {_field.fieldName} {_field.sqlType}", SQLOperationType.a),
                    new SQLOperation($"ALTER TABLE {_field.EntityName} ADD {_field.fieldName} {_field.sqlType}", SQLOperationType.CREATE_COLUMN)
                };

            if (_field.fieldType.Equals(FieldType.REFERENCE))
                return new List<SQLOperation>() { new SQLOperation($"ALTER TABLE {_field.EntityName} ADD {_field.fieldName} {_field.sqlType} FOREIGN KEY REFERENCES {_field.netType} (ID)", SQLOperationType.CREATE_COLUMN)};

        }
    }
}