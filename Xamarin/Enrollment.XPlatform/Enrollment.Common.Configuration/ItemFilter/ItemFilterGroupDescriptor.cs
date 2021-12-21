using System.Collections.Generic;

namespace Enrollment.Common.Configuration.ItemFilter
{
    public class ItemFilterGroupDescriptor : ItemFilterDescriptorBase
    {
        public string Logic { get; set; }
        public ICollection<ItemFilterDescriptorBase> Filters { get; set; }
    }
}
