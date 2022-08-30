using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace Contoso.XPlatform.Converters
{
    public class MenuItemColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Application.Current == null)
                throw new ArgumentException("PrimaryColor");

            bool active = (bool)value;
            if (active)
            {
                if (!Application.Current.Resources.TryGetValue("PrimaryColor", out object color))
                    throw new ArgumentException("PrimaryColor");

                return color;
            }
            else
            {
                if (!Application.Current.Resources.TryGetValue("PrimaryTextColor", out object color))
                    throw new ArgumentException("PrimaryTextColor");

                return color;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
