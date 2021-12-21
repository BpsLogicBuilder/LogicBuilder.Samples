namespace Contoso.Parameters.Expressions
{
    public class SingleOrDefaultOperatorParameters : FilterMethodOperatorParametersBase
    {
		public SingleOrDefaultOperatorParameters()
		{
		}

		public SingleOrDefaultOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}