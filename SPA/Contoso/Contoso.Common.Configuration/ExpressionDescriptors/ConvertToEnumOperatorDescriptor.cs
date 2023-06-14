using System;

namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class ConvertToEnumOperatorDescriptor : OperatorDescriptorBase
    {
		public string Type { get; set; }
		public object ConstantValue { get; set; }
    }
}