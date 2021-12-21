using Contoso.Utils;

namespace Contoso.Forms.Configuration.TextForm.Json
{
    public class LabelItemDescriptorConverter : JsonTypeConverter<LabelItemDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
