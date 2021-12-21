using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Contoso.XPlatform.Converters
{
    public class FirstValidationErrorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return GetFirstError((IDictionary<string, string>)value);

            static string GetFirstError(IDictionary<string, string> errors) 
				=> errors?.FirstOrDefault().Value;
		}

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
			=> null;
    }
}
