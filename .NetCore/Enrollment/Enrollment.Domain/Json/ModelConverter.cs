using Enrollment.Utils;
using System;

namespace Enrollment.Domain.Json
{
    public class ModelConverter : JsonTypeConverter<BaseModelClass>
    {
        public override string TypePropertyName => "TypeFullName";

        protected override Type GetDerivedType(string typeName) 
            => typeof(BaseModelClass).Assembly.GetType(typeName, true, false);
    }
}
