using Contoso.Utils;

namespace Contoso.Forms.Configuration.TextForm.Json
{
    public class SpanItemDescriptorConverter : JsonTypeConverter<SpanItemDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
