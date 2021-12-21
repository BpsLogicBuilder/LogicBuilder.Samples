using Contoso.Common.Configuration.ItemFilter.Json;
using System.Text.Json.Serialization;

namespace Contoso.Common.Configuration.ItemFilter
{
    [JsonConverter(typeof(ItemFilterDescriptorConverter))]
    public abstract class ItemFilterDescriptorBase
    {
        public string TypeString => this.GetType().AssemblyQualifiedName;
    }
}
