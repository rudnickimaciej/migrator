using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class Extensions
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
              || type.Equals(typeof(decimal));
        }

        public static bool IsSingleRefenceType(this Type type)
        {
            return !type.IsSimple() && !type.IsList();
        }
        public static bool IsList(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
        public static bool IsSimpleList(this Type type)
        {
            return type.IsList() && type.GetGenericArguments()[0].IsSimple();
        }
        public static bool IsReferenceList(this Type type)
        {
            return type.IsList() && !type.GetGenericArguments()[0].IsSimple();
        }

        public static string GetNamespace(this Type type)
        {
            return type.IsList() ? type.GetGenericArguments()[0].Namespace : type.Namespace;
        }

        public static Type GetListType<T>(this List<T> _)
        {
            return typeof(T);
        }
    }
}
