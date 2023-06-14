namespace Contoso.Parameters.Expressions
{
    public class SkipOperatorParameters : IExpressionParameter
    {
		public SkipOperatorParameters()
		{
		}

		public SkipOperatorParameters(IExpressionParameter sourceOperand, int count)
		{
			SourceOperand = sourceOperand;
			Count = count;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public int Count { get; set; }
    }
}