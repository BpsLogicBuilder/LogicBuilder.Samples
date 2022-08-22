using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.Utils
{
    public class MultiSelectItemTemplateSelector : DataTemplateSelector
    {
        public MultiSelectItemTemplateSelector()
        {
            /*properties set at initialization*/
            SingleFieldTemplate = null!;
            DoubleFieldTemplate = null!;
        }

        public DataTemplate SingleFieldTemplate { get; set; }
        public DataTemplate DoubleFieldTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return SingleFieldTemplate;
        }
    }
}
