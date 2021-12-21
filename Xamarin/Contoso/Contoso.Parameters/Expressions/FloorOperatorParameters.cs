namespace Contoso.Parameters.Expressions
{
    public class FloorOperatorParameters : IExpressionParameter
    {
		public FloorOperatorParameters()
		{
		}

		public FloorOperatorParameters(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}