using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator.Commons.TypeMapping
{
    internal class TypeMapper
    {
        internal static SQLType ConvertToSQLType(Type type)
        {
            switch (type.Name.ToLower())
            {
                case "int16":
                    return SQLType.SMALLINT;
                case "int32":
                    return SQLType.INT;
                case "int64":
                    return SQLType.BIGINT;
                case "double":
                    return SQLType.FLOAT;
                case "decimal":
                    return SQLType.DECIMAL;
                case "string":
                    return SQLType.VARCHAR;
                case "datetime":
                    return SQLType.DATETIME;
                case "boolean":
                    return SQLType.BIT;
                default:
                    throw new Exception("Brak mapowania typu!");
            }
        }
    }
}
