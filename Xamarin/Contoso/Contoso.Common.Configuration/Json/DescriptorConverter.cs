using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Utils;
using System;

namespace Contoso.Common.Configuration.Json
{
    public class DescriptorConverter : JsonTypeConverter<OperatorDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
