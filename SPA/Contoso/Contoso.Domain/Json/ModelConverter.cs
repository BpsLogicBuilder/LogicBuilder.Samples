using Contoso.Utils;

namespace Contoso.Domain.Json
{
    public class ModelConverter : JsonTypeConverter<EntityModelBase>
    {
        public override string TypePropertyName => "TypeFullName";
    }
}
