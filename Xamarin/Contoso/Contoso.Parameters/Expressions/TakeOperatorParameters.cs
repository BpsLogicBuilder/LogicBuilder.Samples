namespace Contoso.Parameters.Expressions
{
    public class TakeOperatorParameters : IExpressionParameter
    {
		public TakeOperatorParameters()
		{
		}

		public TakeOperatorParameters(IExpressionParameter sourceOperand, int count)
		{
			SourceOperand = sourceOperand;
			Count = count;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public int Count { get; set; }
    }
}