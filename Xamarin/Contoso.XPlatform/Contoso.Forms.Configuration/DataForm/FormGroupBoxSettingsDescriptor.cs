using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DataForm
{
    public class FormGroupBoxSettingsDescriptor : FormItemSettingsDescriptor, IFormGroupBoxSettings
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.GroupBox;

        public string GroupHeader { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public bool IsHidden { get; set; }
    }
}
