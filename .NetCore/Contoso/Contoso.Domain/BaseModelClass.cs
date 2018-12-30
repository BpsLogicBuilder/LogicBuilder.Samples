using Contoso.Domain.Json;
using Contoso.Utils;
using LogicBuilder.Domain;
using Newtonsoft.Json;

namespace Contoso.Domain
{
    [JsonConverter(typeof(ModelConverter))]
    abstract public class BaseModelClass : BaseModel
    {
        public string TypeFullName => this.GetType().ToTypeString();
    }
}
