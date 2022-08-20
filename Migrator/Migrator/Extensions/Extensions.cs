using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Migrator
{
    internal static class Extensions
    {
        public static bool IsSimple(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimple(typeInfo.GetGenericArguments()[0]);
            }
            return typeInfo.IsPrimitive
              || typeInfo.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(decimal))
              || type.Equals(typeof(DateTime));
        }

        internal static bool IsSingleRefenceType(this Type type)
        {
            return !type.IsSimple() && !type.IsList();
        }
        internal static bool IsList(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
        internal static bool IsSimpleList(this Type type)
        {
            return type.IsList() && type.GetGenericArguments()[0].IsSimple();
        }
        internal static bool IsReferenceList(this Type type)
        {
            return type.IsList() && !type.GetGenericArguments()[0].IsSimple();
        }
        internal static string GetNamespace(this Type type)
        {
            return type.IsList() ? type.GetGenericArguments()[0].Namespace : type.Namespace;
        }

        internal static Type GetListType<T>(this List<T> _)
        {
            return typeof(T);
        }

        public static IEnumerable<T> Select<T>(this IDataReader reader,
                                       Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
    }
}
