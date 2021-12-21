using System;

namespace Contoso.Parameters.Expressions
{
    public class CollectionCastOperatorParameters : IExpressionParameter
    {
		public CollectionCastOperatorParameters()
		{
		}

		public CollectionCastOperatorParameters(IExpressionParameter operand, Type type)
		{
			Operand = operand;
			Type = type;
		}

		public IExpressionParameter Operand { get; set; }
		public Type Type { get; set; }
    }
}