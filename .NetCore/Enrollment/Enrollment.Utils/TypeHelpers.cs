using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Enrollment.Utils
{
    public static class TypeHelpers
    {
        private static HashSet<Type> ValidGenericListTypes => new HashSet<Type>(new Type[]
        {
            typeof(List<>),
            typeof(IList<>),
            typeof(Collection<>),
            typeof(ICollection<>),
            typeof(IEnumerable<>)
        });

        public static bool IsValidList(this Type type)
        {
            return
            (
                (type.IsGenericType && ValidGenericListTypes.Contains(type.GetGenericTypeDefinition()))
                ||
                (type.IsArray && type.GetArrayRank() == 1)
            );
        }

        public static Type GetUndelyingTypeForValidList(this Type type)
        {
            if (type.IsGenericType && ValidGenericListTypes.Contains(type.GetGenericTypeDefinition()))
                return type.GetGenericArguments()[0];
            else if (type.IsArray)
                return type.GetElementType();
            else
                throw new ArgumentException(Properties.Resources.invalidListTypeFormat.FormatString(type.ToString()));
        }

        public static bool IsNullable(this Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));


        public static Type GetListType(this Type listType, Type elementType)
        {
            if (!listType.IsValidList())
                throw new ArgumentException(Properties.Resources.invalidListTypeFormat.FormatString(listType.ToString()));

            if (listType.IsArray)
                return elementType.MakeArrayType();

            return listType.GetGenericTypeDefinition().MakeGenericType(elementType);
        }

        public static bool IsLiteralType(this Type type)
        {
            if (type.IsNullable())
                type = Nullable.GetUnderlyingType(type);

            return Literals.Contains(type);
        }

        private static readonly HashSet<Type> Literals = new HashSet<Type>
        {
            typeof(bool),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(Guid),
            typeof(decimal),
            typeof(byte),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(char),
            typeof(sbyte),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(string)
        };

        public static string ToTypeString(this Type type)
            => type.IsGenericType && !type.IsGenericTypeDefinition
                ? type.AssemblyQualifiedName
                : type.FullName;
    }
}
