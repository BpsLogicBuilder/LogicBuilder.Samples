using System;

namespace Contoso.Parameters.Expressions
{
    public class ConstantOperatorParameters : IExpressionParameter
    {
		public ConstantOperatorParameters()
		{
		}

		public ConstantOperatorParameters(object constantValue, Type type = null)
		{
			ConstantValue = constantValue;
			Type = type;
		}

		public Type Type { get; set; }
		public object ConstantValue { get; set; }
    }
}