using Migrator.Commons;
using Migrator.ISQLProviderNamespace;
using System.Collections.Generic;

namespace Migrator.SQLServerProviderNamespace.SQLOperations
{
    internal class ModifyFieldTypeOperation : SQLOperation
    {
        public override SQLOperationType Type => SQLOperationType.ALTER_COLUMN_TYPE;

        public ModifyFieldTypeOperation(TFieldModel field)
        {
            bool fieldLength = field.FieldLength != -1;
            bool fieldRequired = field.IsRequired;

            Sql = "ALTER TABLE " + field.EntityName +
                  " ALTER COLUMN " + field.Name + " " + field.SqlType + (fieldLength ? "(" + field.FieldLength + ")" : " ") +
                  " " + (fieldRequired ? "NOT NULL " : " ");
        }


        private object map(object value, SQLType sqlType)
        {
            Dictionary<SQLType, IValueMapper> _dict = new Dictionary<SQLType, IValueMapper>()
            {
                { SQLType.INT, new DefaultMapper() },
                { SQLType.VARCHAR, new StringMapper() },
                { SQLType.BIT, new BoolMapper() }
            };

            return _dict[sqlType].Map(value);
        }
    }

    internal interface IValueMapper
    {
        object Map(object value);
    }
    internal class DefaultMapper : IValueMapper
    {
        public object Map(object value) => value;
    }
    internal class IntMapper : IValueMapper
    {
        public object Map(object value)
        {
            throw new System.NotImplementedException();
        }
    }
    internal class StringMapper : IValueMapper
    {
        public object Map(object value) => "'" + value.ToString() + "'";
    }
    internal class BoolMapper : IValueMapper
    {
        public object Map(object value)
        {
            throw new System.NotImplementedException();
        }
    }
}