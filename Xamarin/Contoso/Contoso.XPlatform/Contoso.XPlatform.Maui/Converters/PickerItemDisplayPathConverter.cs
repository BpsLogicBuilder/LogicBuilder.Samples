using Contoso.Forms.Configuration;
using Contoso.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.Converters
{
    public class PickerItemDisplayPathConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return null!;/*ok to return null for converters*/

            object bindingContext = ((VisualElement)parameter!).BindingContext;
            var dropDownTemplate = bindingContext.GetPropertyValue<DropDownTemplateDescriptor>
            (
                nameof(PickerValidatableObject<int>.DropDownTemplate)
            );

            return value.GetPropertyValue<string>(dropDownTemplate.TextField);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
