using FontAwesome.UWP;
using System;
using Windows.UI.Xaml.Data;

namespace CheckMySymptoms.Utils
{
    public class FontAwesomeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Enum.IsDefined(typeof(FontAwesomeIcon), value ?? ""))
                return Enum.Parse(typeof(FontAwesomeIcon), value.ToString());

#if(DEBUG)
            if (value == null)
                throw new ArgumentException("FontAwesome icon cannot be empty empty.");
            else
                throw new ArgumentException($"FontAwesome icon {0} is invalid.", value.ToString());
#else
            return FontAwesomeIcon.None;
#endif
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
