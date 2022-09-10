using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using Migrator.SQLServerProviderNamespace.SQLOperations;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLActions
{
    public class AddFieldAction : ISQLAction
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
                    new AddFieldOperation(_field.EntityName, _field.Name, SQLType.INT,_field.FieldLength),
                    new AddFKOperation(_field.EntityName, _field.Name, _field.NetType) };

            if (_field.Type.Equals(FieldType.SIMPLE_LIST))
            {
                return new List<SQLOperation>(){
                    new CreateTableOperation($"{_field.EntityName}_{_field.Name}"),
                    new AddFieldOperation($"{_field.EntityName}_{_field.Name}", $"{_field.EntityName}Id",SQLType.INT,_field.FieldLength),
                    new AddFieldOperation($"{_field.EntityName}_{_field.Name}", $"Value",_field.SqlType,_field.FieldLength),
                    new AddFKOperation($"{_field.EntityName}_{_field.Name}",$"{_field.EntityName}Id",$"{_field.EntityName}") };
            }

            if (_field.Type.Equals(FieldType.REFERENCE_LIST))
                return new List<SQLOperation>(){
                    new CreateTableOperation($"{_field.EntityName}_{_field.NetType}"),
                    new AddFieldOperation($"{_field.EntityName}_{_field.NetType}", $"{_field.EntityName}Id",SQLType.INT, _field.FieldLength),
                    new AddFieldOperation($"{_field.EntityName}_{_field.NetType}", $"{_field.NetType}Id",SQLType.INT, _field.FieldLength),
                    new AddFKOperation($"{_field.EntityName}_{_field.NetType}",$"{_field.EntityName}Id",$"{_field.EntityName}"),
                    new AddFKOperation($"{_field.EntityName}_{_field.NetType}",$"{_field.NetType}Id",$"{_field.NetType}") };

            return null;
        }
    }
}