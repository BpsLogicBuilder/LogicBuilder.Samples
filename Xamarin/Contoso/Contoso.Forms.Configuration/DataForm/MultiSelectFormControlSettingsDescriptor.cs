using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DataForm
{
    public class MultiSelectFormControlSettingsDescriptor : FormControlSettingsDescriptor
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.MultiSelectFormControl;
        public List<string> KeyFields { get; set; }
        public MultiSelectTemplateDescriptor MultiSelectTemplate { get; set; }
    }
}
