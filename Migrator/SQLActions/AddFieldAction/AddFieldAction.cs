using System.Collections.Generic;

namespace Migrator
{
    internal class AddFieldAction : ISQLAction
    {
        private readonly TFieldModel _field;

        public AddFieldAction(TFieldModel field) => _field = field;

        public List<SQLOperation> GenerateOperations()
        {
            if (_field.Type.Equals(FieldType.SIMPLE))
                return new List<SQLOperation>(){
                    new AddFieldOperation(_field)};

            if (_field.Type.Equals(FieldType.REFERENCE))
                return new List<SQLOperation>(){
                    new AddFieldOperation(_field),
                    new AddFKOperation(_field) };

            if (_field.Type.Equals(FieldType.SIMPLE_LIST))
                return new List<SQLOperation>(){
                    new CreateTableOperation($"{_field.EntityName}_{_field.Name}"),
                    new AddFKOperation(_field) };

            if (_field.Type.Equals(FieldType.REFERENCE_LIST))
                return new List<SQLOperation>(){
                    new CreateTableOperation($"{_field.EntityName}_{_field.Name}"),
                    new AddFKOperation(_field),
                    new AddFKOperation(_field) };

            return null;
        }
    }
}