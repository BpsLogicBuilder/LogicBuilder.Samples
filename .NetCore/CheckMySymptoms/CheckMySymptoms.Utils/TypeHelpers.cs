using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CheckMySymptoms.Utils
{
    public static class TypeHelpers
    {
        public static string ToTypeString(this Type type)
            => type.IsGenericType && !type.IsGenericTypeDefinition
                ? type.AssemblyQualifiedName
                : type.FullName;

        public static bool TryParse(this string toParse, Type type, out object result)
        {
            if (type == null)
            {
                result = null;
                return false;
            }

            if (type == typeof(string))
            {
                result = toParse;
                return true;
            }

            if (type.IsNullable())
                type = Nullable.GetUnderlyingType(type);

            MethodInfo method = type.GetMethods(BindingFlags.Public | BindingFlags.Static).SingleOrDefault
            (
                s => 
                {
                    if (s.Name != "TryParse")
                        return false;

                    ParameterInfo[] parameters = s.GetParameters();

                    if (parameters.Length != 2)
                        return false;

                    return parameters[0].ParameterType == typeof(string);
                }
            );

            if (method == null)
            {
                result = null;
                return false;
            }

            object[] args = new object[] { toParse, null };
            bool success = (bool)method.Invoke(null, args);
            result = success ? args[1] : null;

            return success;
        }

        public static bool IsNullable(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));

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
    }
}
