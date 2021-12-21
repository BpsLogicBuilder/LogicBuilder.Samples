using Contoso.Utils;
using LogicBuilder.Domain;
using System;

namespace Contoso.Domain
{
    abstract public class BaseModelClass : BaseModel
    {
        public string TypeFullName => this.GetType().AssemblyQualifiedName;
    }
}
