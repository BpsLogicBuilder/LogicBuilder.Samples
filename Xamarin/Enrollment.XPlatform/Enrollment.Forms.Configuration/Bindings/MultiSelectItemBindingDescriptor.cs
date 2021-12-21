using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.Bindings
{
    public class MultiSelectItemBindingDescriptor : ItemBindingDescriptor
    {
        public List<string> KeyFields { get; set; }
        public MultiSelectTemplateDescriptor MultiSelectTemplate { get; set; }

        public override string TemplateName => MultiSelectTemplate.TemplateName;
    }
}
