namespace Contoso.Parameters.Expressions
{
    public class CeilingOperatorParameters : IExpressionParameter
    {
		public CeilingOperatorParameters()
		{
		}

		public CeilingOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}