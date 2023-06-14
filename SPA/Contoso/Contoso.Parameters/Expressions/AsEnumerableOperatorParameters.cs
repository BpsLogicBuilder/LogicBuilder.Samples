namespace Contoso.Parameters.Expressions
{
    public class AsEnumerableOperatorParameters : IExpressionParameter
    {
		public AsEnumerableOperatorParameters()
		{
		}

		public AsEnumerableOperatorParameters(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}