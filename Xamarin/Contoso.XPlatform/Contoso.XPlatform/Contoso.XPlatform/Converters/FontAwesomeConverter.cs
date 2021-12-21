using Contoso.XPlatform.Utils;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Contoso.XPlatform.Converters
{
    public class FontAwesomeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (FontAwesomeIcons.Solid.TryGetValue(value?.ToString() ?? "", out string icon))
                return icon;

#if(DEBUG)
            if (value == null)
                throw new ArgumentException("FontAwesome icon cannot be empty empty.");
            else
                throw new ArgumentException($"FontAwesome icon {0} is invalid.", value.ToString());
#else
            return "\uf015";// FontAwesomeIcon.None;
#endif
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
