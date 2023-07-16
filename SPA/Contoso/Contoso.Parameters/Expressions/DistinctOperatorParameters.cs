namespace Contoso.Parameters.Expressions
{
    public class DistinctOperatorParameters : IExpressionParameter
    {
		public DistinctOperatorParameters()
		{
		}

		public DistinctOperatorParameters(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}