using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Contoso.Utils
{
    public static class StringHelpers
    {
        public static string FormatString(this string format, params object[] args) => string.Format(CultureInfo.CurrentCulture, format, args);
        public static string FormatString(this string format, IFormatProvider provider, params object[] args) => string.Format(provider, format, args);
    }
}
