namespace Contoso.Parameters.Expressions
{
    public class HasOperatorParameters : IExpressionParameter
    {
		public HasOperatorParameters()
		{
		}

		public HasOperatorParameters(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}