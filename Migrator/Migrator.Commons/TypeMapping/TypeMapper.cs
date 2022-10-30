using System;

namespace Migrator.Commons.TypeMapping
{
    internal class TypeMapper
    {
        internal static SQLType ConvertToSQLType(Type type)
        {
            switch (type.Name.ToLower())
            {
                case Consts.INT16:
                    return SQLType.SMALLINT;

                case Consts.INT32:
                    return SQLType.INT;

                case Consts.INT64:
                    return SQLType.BIGINT;

                case Consts.DECIMAL:
                    return SQLType.DECIMAL;

                case Consts.DOUBLE:
                    return SQLType.FLOAT;

                case Consts.FLOAT:
                    return SQLType.FLOAT;

                case Consts.STRING:
                    return SQLType.VARCHAR;

                case Consts.CHAR:
                    return SQLType.CHAR;

                case Consts.DATETIME:
                    return SQLType.DATETIME;

                case Consts.BOOL:
                    return SQLType.BIT;

                default:
                    throw new Exception("Brak mapowania typu!");
            }
        }

        internal static int GetTypeDefaultLength(Type type)
        {
            switch (type.Name.ToLower())
            {
                case Consts.DECIMAL:
                    return 10;

                case Consts.FLOAT:
                    return 53;

                case Consts.STRING:
                    return 101;

                case Consts.CHAR:
                    return 102;

                case Consts.DATETIME:
                    return -1;

                default:
                    return -1;
            }
        }
    }
}
