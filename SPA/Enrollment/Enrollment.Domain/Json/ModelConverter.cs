using Enrollment.Utils;

namespace Enrollment.Domain.Json
{
    public class ModelConverter : JsonTypeConverter<EntityModelBase>
    {
        public override string TypePropertyName => "TypeFullName";
    }
}
