using System;
using System.Globalization;

namespace Contoso.Forms.View
{
    internal static class Helpers
    {
        internal static string ToTypeName(this Type type)
            => type.IsGenericType ? type.AssemblyQualifiedName : type.FullName;

        internal static string FormatString(this string format, params object[] args) => string.Format(CultureInfo.CurrentCulture, format, args);
        internal static string FormatString(this string format, IFormatProvider provider, params object[] args) => string.Format(provider, format, args);
    }
}
