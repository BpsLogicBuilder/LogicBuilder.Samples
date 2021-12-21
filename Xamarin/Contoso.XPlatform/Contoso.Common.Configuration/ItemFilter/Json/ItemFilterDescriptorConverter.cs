using Contoso.Utils;

namespace Contoso.Common.Configuration.ItemFilter.Json
{
    public class ItemFilterDescriptorConverter : JsonTypeConverter<ItemFilterDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
