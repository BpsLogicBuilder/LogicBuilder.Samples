using Enrollment.Domain.Json;
using Enrollment.Utils;
using LogicBuilder.Domain;
using Newtonsoft.Json;

namespace Enrollment.Domain
{
    [JsonConverter(typeof(ModelConverter))]
    abstract public class BaseModelClass : BaseModel
    {
        public string TypeFullName => this.GetType().ToTypeString();
    }
}
