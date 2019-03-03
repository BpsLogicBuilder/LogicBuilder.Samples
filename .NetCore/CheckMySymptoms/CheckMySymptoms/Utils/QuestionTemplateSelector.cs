using CheckMySymptoms.Forms.View.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CheckMySymptoms.Utils
{
    public class QuestionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DropDownTemplate { get; set; }
        public DataTemplate MultiSelectTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate DateTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            BaseInputView input = (BaseInputView)item;
            if (input.DropDownTemplate != null)
                return DropDownTemplate;

            if (input.MultiSelectTemplate != null)
                return MultiSelectTemplate;

            if (input.TextTemplate != null)
            {
                switch(input.TextTemplate.TemplateName)
                {
                    case "dateTemplate":
                        return DateTemplate;
                    default:
                        return TextTemplate;
                }
            }

            return TextTemplate;
        }
    }
}
