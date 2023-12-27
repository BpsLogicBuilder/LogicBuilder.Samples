using Enrollment.XPlatform.Constants;
using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace Enrollment.XPlatform.Converters
{
    public class MenuItemColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Application.Current == null)
                throw new ArgumentException(nameof(Application.Current));

            bool active = (bool)value!;
            if (active)
            {
                if (!Application.Current.Resources.TryGetValue(ColorKeys.PrimaryColor, out object color))
                    throw new ArgumentException(ColorKeys.PrimaryColor);

                return color;
            }
            else
            {
                if (!Application.Current.Resources.TryGetValue(ColorKeys.PrimaryTextColor, out object color))
                    throw new ArgumentException(ColorKeys.PrimaryTextColor);

                return color;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
