using Contoso.Utils;
using System;

namespace Contoso.Domain.Json
{
    public class ModelConverter : JsonTypeConverter<BaseModelClass>
    {
        

        public override string TypePropertyName => "TypeFullName";


        protected override Type GetDerivedType(string typeName)
        {
            return typeof(BaseModelClass).Assembly.GetType(typeName, true, false);
        }
    }
}
