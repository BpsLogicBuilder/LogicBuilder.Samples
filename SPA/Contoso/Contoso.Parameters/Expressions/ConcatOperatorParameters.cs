namespace Contoso.Parameters.Expressions
{
    public class ConcatOperatorParameters : IExpressionParameter
    {
		public ConcatOperatorParameters()
		{
		}

		public ConcatOperatorParameters(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}