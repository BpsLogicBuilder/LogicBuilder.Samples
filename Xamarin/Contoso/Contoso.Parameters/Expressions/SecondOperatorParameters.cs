namespace Contoso.Parameters.Expressions
{
    public class SecondOperatorParameters : IExpressionParameter
    {
		public SecondOperatorParameters()
		{
		}

		public SecondOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}