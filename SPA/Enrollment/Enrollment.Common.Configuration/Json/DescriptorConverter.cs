using Enrollment.Common.Configuration.ExpressionDescriptors;
using Enrollment.Utils;
using System;

namespace Enrollment.Common.Configuration.Json
{
    public class DescriptorConverter : JsonTypeConverter<OperatorDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
