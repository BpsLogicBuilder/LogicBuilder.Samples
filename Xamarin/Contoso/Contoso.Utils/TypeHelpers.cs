using LogicBuilder.Attributes;
using LogicBuilder.Expressions.Utils;
using System;
using System.Linq;
using System.Reflection;

namespace Contoso.Utils
{
    public static class TypeHelpers
    {
        public static string ToTypeString(this Type type)
            => type.IsGenericType && !type.IsGenericTypeDefinition
                ? type.AssemblyQualifiedName
                : type.FullName;

        [AlsoKnownAs("Get Type")]
        public static Type GetType([ParameterEditorControl(ParameterControlType.TypeAutoComplete)] string assemblyQualifiedTypeName)
            => Type.GetType(assemblyQualifiedTypeName);

        public static object GetPropertyValue(this object item, string propertyName)
        {
            try
            {
                return item.GetType().GetProperty
                (
                    propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
                )
                .GetValue(item);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{ ex.GetType().Name + " : " + ex.Message}");
                throw;
            }
        }


        public static T GetPropertyValue<T>(this object item, string propertyName)
        {
            try
            {
                return item.GetType().GetProperty
                (
                    propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
                )
                .GetValue(item)
                .GetPropertyValue<T>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{ ex.GetType().Name + " : " + ex.Message}");
                throw;
            }
        }

        public static bool TryParse(this string toParse, Type type, out object result)
        {
            if (type == null)
                throw new ArgumentException($"{nameof(type)}: DD23F248-BA7B-42BD-B825-8EAE7715DF35");

            if (type == typeof(string))
            {
                result = toParse;
                return true;
            }

            if (typeof(Enum).IsAssignableFrom(type))
            {
                if (!int.TryParse(toParse, out int _) && !Enum.IsDefined(type, toParse))
                {
                    result = null;
                    return false;
                }

                result = Enum.Parse(type, toParse);
                return true;
            }

            if (type.IsNullableType())
                type = Nullable.GetUnderlyingType(type);

            MethodInfo method = type.GetMethods().First(MatchTryParseMethod);

            bool MatchTryParseMethod(MethodInfo info)
            {
                if (info.Name != "TryParse")
                    return false;

                ParameterInfo[] pInfos = info.GetParameters();
                if (pInfos.Length != 2)
                    return false;

                return pInfos[0].ParameterType == typeof(string)
                    && pInfos[1].IsOut
                    && pInfos[1].ParameterType.GetElementType() == type;
            }

            object[] args = new object[] { toParse, null };
            bool success = (bool)method.Invoke(null, args);
            result = success ? args[1] : null;

            return success;
        }

        private static T GetPropertyValue<T>(this object valueObject)
        {
            if (valueObject == null)
                return default;

            Type valueObjectType = valueObject.GetType();
            if (typeof(T) == valueObjectType)
                return (T)valueObject;

            return (T)Convert.ChangeType(valueObject, typeof(T));
        }
    }
}
