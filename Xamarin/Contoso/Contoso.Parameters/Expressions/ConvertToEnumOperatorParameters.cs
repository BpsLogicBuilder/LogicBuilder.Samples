using System;

namespace Contoso.Parameters.Expressions
{
    public class ConvertToEnumOperatorParameters : IExpressionParameter
    {
		public ConvertToEnumOperatorParameters()
		{
		}

		public ConvertToEnumOperatorParameters(object constantValue, Type type)
		{
			ConstantValue = constantValue;
			Type = type;
		}

		public Type Type { get; set; }
		public object ConstantValue { get; set; }
    }
}