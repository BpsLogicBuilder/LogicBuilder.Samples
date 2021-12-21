using Contoso.Utils;

namespace Contoso.Forms.Configuration.SearchForm.Json
{
    public class SearchFilterDescriptorConverter : JsonTypeConverter<SearchFilterDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
