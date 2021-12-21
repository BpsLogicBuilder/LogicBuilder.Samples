using Enrollment.Utils;

namespace Enrollment.Forms.Configuration.TextForm.Json
{
    public class SpanItemDescriptorConverter : JsonTypeConverter<SpanItemDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
