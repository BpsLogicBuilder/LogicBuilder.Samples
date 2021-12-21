using Enrollment.Utils;

namespace Enrollment.Common.Configuration.ItemFilter.Json
{
    public class ItemFilterDescriptorConverter : JsonTypeConverter<ItemFilterDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
