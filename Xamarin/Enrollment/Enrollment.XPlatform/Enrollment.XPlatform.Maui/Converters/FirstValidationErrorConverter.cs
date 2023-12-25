using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Enrollment.XPlatform.Converters
{
    public class FirstValidationErrorConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			return GetFirstError((IDictionary<string, string>?)value);

            static string? GetFirstError(IDictionary<string, string>? errors) 
				=> errors?.FirstOrDefault().Value;/*ok to return null for converters*/
		}

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
