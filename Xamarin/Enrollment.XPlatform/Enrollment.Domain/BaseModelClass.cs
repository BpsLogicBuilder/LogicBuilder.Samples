using LogicBuilder.Domain;

namespace Enrollment.Domain
{
    abstract public class BaseModelClass : BaseModel
    {
        public string TypeFullName => this.GetType().AssemblyQualifiedName;
    }
}
