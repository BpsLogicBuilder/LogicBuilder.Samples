using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class FormGroupBoxSettingsView : FormItemSettingView
    {
        public override AbstractControlEnum AbstractControlType { get; set; }
        public FormGroupTemplateView FormGroupTemplate { get; set; }
        public List<FormItemSettingView> FieldSettings { get; set; }
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
    }
}
