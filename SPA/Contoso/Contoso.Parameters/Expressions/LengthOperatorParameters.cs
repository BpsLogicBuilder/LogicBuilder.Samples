namespace Contoso.Parameters.Expressions
{
    public class LengthOperatorParameters : IExpressionParameter
    {
		public LengthOperatorParameters()
		{
		}

		public LengthOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}