using Contoso.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Contoso.XPlatform.Converters
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetFormattedString(GetStringFormat());

            object GetFormattedString(string stringFormat)
            {
                if (value == null)
                    return null;

                if (string.IsNullOrEmpty(stringFormat))
                    return value;

                return string.Format(CultureInfo.CurrentCulture, stringFormat, value);
            }

            string GetStringFormat()
                => ((VisualElement)parameter).BindingContext.GetPropertyValue<string>
                (
                    nameof(EntryValidatableObject<string>.StringFormat)
                );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().TryParse(targetType, out object result))
                return result;

            return value;
        }
    }
}
