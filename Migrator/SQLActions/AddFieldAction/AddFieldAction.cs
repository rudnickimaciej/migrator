using System.Collections.Generic;

namespace Migrator
{
    internal class AddFieldAction : ISQLAction
    {
        private readonly XMLModelField _field;

        public AddFieldAction(XMLModelField field) => _field = field;

        public SQLOperationCollection GenerateOperations()
        {
            if (_field.Type.Equals(FieldType.SIMPLE))
                return new SQLOperationCollection(
                    new AddFieldOperation(_field));

            if (_field.Type.Equals(FieldType.REFERENCE))
                return new SQLOperationCollection(
                    new AddFieldOperation(_field),
                    new AddFKOperation(_field));

            if (_field.Type.Equals(FieldType.SIMPLE_LIST))
                return new SQLOperationCollection(
                    new CreateTableOperation($"{_field.EntityName}_{_field.Name}"),
                    new AddFKOperation(_field));

            if (_field.Type.Equals(FieldType.REFERENCE_LIST))
                return new SQLOperationCollection(
                    new CreateTableOperation($"{_field.EntityName}_{_field.Name}"),
                    new AddFKOperation(_field),
                    new AddFKOperation(_field);

            return null;
        }
    }
}