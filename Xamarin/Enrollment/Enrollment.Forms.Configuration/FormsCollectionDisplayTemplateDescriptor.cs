using Enrollment.Forms.Configuration.Bindings;
using System.Collections.Generic;

namespace Enrollment.Forms.Configuration
{
    public class FormsCollectionDisplayTemplateDescriptor
    {
        public string TemplateName { get; set; }
        public Dictionary<string, ItemBindingDescriptor> Bindings { get; set; }
    }
}
