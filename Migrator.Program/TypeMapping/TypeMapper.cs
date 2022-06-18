using Migrator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal class TypeMapper
    {
        internal static SQLType ConvertToSQLType(Type type)
        {
            switch (type.Name.ToLower())
            {
                case "int32":
                    return SQLType.INT;
                case "string":
                    return SQLType.VARCHAR;
                case "datetime":
                    return SQLType.DATETIME;
                case "boolean":
                    return SQLType.BOOL;
                default:
                    throw new Exception("Brak mapowania typu!");
            }
        }

        internal static Type SQLTypeToNetType(string sqlType)
        {
            switch (sqlType.ToLower())
            {
                case "int":
                    return typeof(System.Int32);
                case "varchar(30)":
                    return typeof(System.String);
                case "datetime":
                    return typeof(System.DateTime);
                case "bool":
                    return typeof(System.Boolean);
                default:
                    throw new Exception("Brak mapowania typu!");
            }

        }
    }
}
