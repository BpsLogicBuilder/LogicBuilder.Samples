using CheckMySymptoms.Utils;
using LogicBuilder.Domain;

namespace CheckMySymptoms.Domain
{
    //[JsonConverter(typeof(ModelConverter))]
    abstract public class BaseModelClass : BaseModel
    {
        public string TypeFullName => this.GetType().ToTypeString();
    }
}
