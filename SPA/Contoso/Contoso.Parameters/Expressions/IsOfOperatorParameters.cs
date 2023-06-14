using System;

namespace Contoso.Parameters.Expressions
{
    public class IsOfOperatorParameters : IExpressionParameter
    {
		public IsOfOperatorParameters()
		{
		}

		public IsOfOperatorParameters(IExpressionParameter operand, Type type)
		{
			Operand = operand;
			Type = type;
		}

		public IExpressionParameter Operand { get; set; }
		public Type Type { get; set; }
    }
}