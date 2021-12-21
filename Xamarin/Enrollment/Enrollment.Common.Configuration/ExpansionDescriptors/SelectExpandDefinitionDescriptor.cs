using System.Collections.Generic;

namespace Enrollment.Common.Configuration.ExpansionDescriptors
{
    public class SelectExpandDefinitionDescriptor
    {
        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemDescriptor> ExpandedItems { get; set; } = new List<SelectExpandItemDescriptor>();
    }
}
