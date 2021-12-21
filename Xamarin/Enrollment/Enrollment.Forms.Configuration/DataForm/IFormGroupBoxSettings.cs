using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.DataForm
{
    public interface IFormGroupBoxSettings
    {
        string GroupHeader { get; }
        List<FormItemSettingsDescriptor> FieldSettings { get; }
        MultiBindingDescriptor HeaderBindings { get; }
        bool IsHidden { get; }
    }
}
