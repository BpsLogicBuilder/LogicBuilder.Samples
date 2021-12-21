using Enrollment.Utils;

namespace Enrollment.Forms.Configuration.SearchForm.Json
{
    public class SearchFilterDescriptorConverter : JsonTypeConverter<SearchFilterDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
