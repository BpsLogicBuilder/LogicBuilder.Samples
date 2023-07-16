using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class MultiSelectFormControlSettingsView : FormControlSettingsView
    {
        public override AbstractControlEnum AbstractControlType { get; set; }
        public List<string> KeyFields { get; set; }
        public MultiSelectTemplateView MultiSelectTemplate { get; set; }
    }
}