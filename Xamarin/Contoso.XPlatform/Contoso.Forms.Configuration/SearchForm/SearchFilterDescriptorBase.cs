using Contoso.Forms.Configuration.SearchForm.Json;
using System.Text.Json.Serialization;

namespace Contoso.Forms.Configuration.SearchForm
{
    [JsonConverter(typeof(SearchFilterDescriptorConverter))]
    public abstract class SearchFilterDescriptorBase
    {
        public string TypeString => this.GetType().AssemblyQualifiedName;
    }
}
