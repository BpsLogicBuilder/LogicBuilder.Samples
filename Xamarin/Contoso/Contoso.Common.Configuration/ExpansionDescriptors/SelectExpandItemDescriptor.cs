using System.Collections.Generic;

namespace Contoso.Common.Configuration.ExpansionDescriptors
{
    public class SelectExpandItemDescriptor
    {
        public string MemberName { get; set; }
        public SelectExpandItemFilterDescriptor Filter { get; set; }
        public SelectExpandItemQueryFunctionDescriptor QueryFunction { get; set; }
        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemDescriptor> ExpandedItems { get; set; } = new List<SelectExpandItemDescriptor>();
    }
}
