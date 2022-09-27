using System;
using Xamarin.Forms;

namespace Contoso.XPlatform.Utils
{
    public class MultiSelectItemTemplateSelector : DataTemplateSelector
    {
        private DataTemplate? _singleFieldTemplate;
        private DataTemplate? _doubleFieldTemplate;

        public DataTemplate SingleFieldTemplate
        {
            get => _singleFieldTemplate ?? throw new ArgumentException($"{nameof(_singleFieldTemplate)}: {{E7AEC16C-E94C-489A-9BF4-4D3DCE1226EC}}");
            set => _singleFieldTemplate = value;
        }

        public DataTemplate DoubleFieldTemplate
        {
            get => _doubleFieldTemplate ?? throw new ArgumentException($"{nameof(_doubleFieldTemplate)}: {{F5F03AEA-E2D4-4B3E-9B3D-48A1DD9DB799}}");
            set => _doubleFieldTemplate = value;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return SingleFieldTemplate;
        }
    }
}
