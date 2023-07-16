namespace Contoso.Parameters.Expressions
{
    public class AsQueryableOperatorParameters : IExpressionParameter
    {
		public AsQueryableOperatorParameters()
		{
		}

		public AsQueryableOperatorParameters(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}