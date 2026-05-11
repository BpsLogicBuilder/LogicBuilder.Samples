using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Contoso.XPlatform.Utils
{
    internal static class TypeHelpers
    {
        private static readonly Dictionary<Type, HashSet<Type>> NumbersDictionary = new()
        {
            { typeof(decimal), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong) } },
            { typeof(double), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float) } },
            { typeof(float), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong) } },
            { typeof(ulong), new HashSet<Type> { typeof(byte), typeof(char), typeof(ushort), typeof(uint) } },
            { typeof(long), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort), typeof(int), typeof(uint) } },
            { typeof(uint), new HashSet<Type> { typeof(byte), typeof(char), typeof(ushort) } },
            { typeof(int), new HashSet<Type> { typeof(byte), typeof(sbyte), typeof(char), typeof(short), typeof(ushort) } },
            { typeof(ushort), new HashSet<Type> { typeof(byte), typeof(char) } },
            { typeof(short), new HashSet<Type> { typeof(byte), typeof(sbyte) } }
        };

        public static bool AssignableFrom(Type to, Type from)
        {
            if (to.IsAssignableFrom(from))
                return true;

            if (!(!to.IsNullable() && from.IsNullable()))
            {//Anything but To is NOT nullable and From IS nullable
                to = to.IsNullable() ? Nullable.GetUnderlyingType(to)! : to;
                from = from.IsNullable() ? Nullable.GetUnderlyingType(from)! : from;

                if (NumbersDictionary.TryGetValue(to, out HashSet<Type>? toConversions) && toConversions.Contains(from))
                    return true;
            }

            bool ReturnTypeValid(Type returnType) => returnType == to || (NumbersDictionary.TryGetValue(to, out HashSet<Type>? toConversions) && toConversions.Contains(returnType));
            bool ParameterValid(Type parameterType) => (parameterType == from) || (NumbersDictionary.TryGetValue(parameterType, out HashSet<Type>? parameterTypeConversions) && parameterTypeConversions.Contains(from));
            bool MatchImplicitOperator(MethodInfo m) => m.Name == "op_Implicit"
                                                        && ReturnTypeValid(m.ReturnType)
                                                        && ParameterValid(m.GetParameters().Single().ParameterType);

            return from.GetMethods(BindingFlags.Public | BindingFlags.Static).Any(MatchImplicitOperator)
                    || to.GetMethods(BindingFlags.Public | BindingFlags.Static).Any(MatchImplicitOperator);
        }

        static bool IsNullable(this Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
    }
}
