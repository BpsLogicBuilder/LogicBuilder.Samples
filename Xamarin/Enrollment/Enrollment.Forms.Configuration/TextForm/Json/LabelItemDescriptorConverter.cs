using Enrollment.Utils;

namespace Enrollment.Forms.Configuration.TextForm.Json
{
    public class LabelItemDescriptorConverter : JsonTypeConverter<LabelItemDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
