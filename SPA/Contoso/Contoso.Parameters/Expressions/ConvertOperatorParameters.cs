using System;

namespace Contoso.Parameters.Expressions
{
    public class ConvertOperatorParameters : IExpressionParameter
    {
		public ConvertOperatorParameters()
		{
		}

		public ConvertOperatorParameters(IExpressionParameter sourceOperand, Type type)
		{
			SourceOperand = sourceOperand;
			Type = type;
		}

		public Type Type { get; set; }
		public IExpressionParameter SourceOperand { get; set; }
    }
}