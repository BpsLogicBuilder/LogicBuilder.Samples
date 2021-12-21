using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    public class MultiSelectItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SingleFieldTemplate { get; set; }
        public DataTemplate DoubleFieldTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return SingleFieldTemplate;
        }
    }
}
