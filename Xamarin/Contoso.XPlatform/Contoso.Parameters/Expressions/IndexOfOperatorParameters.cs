namespace Contoso.Parameters.Expressions
{
    public class IndexOfOperatorParameters : IExpressionParameter
    {
		public IndexOfOperatorParameters()
		{
		}

		public IndexOfOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter itemToFind)
		{
			SourceOperand = sourceOperand;
			ItemToFind = itemToFind;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public IExpressionParameter ItemToFind { get; set; }
    }
}