namespace Contoso.Parameters.Expressions
{
    public class LastOrDefaultOperatorParameters : FilterMethodOperatorParametersBase
    {
		public LastOrDefaultOperatorParameters()
		{
		}

		public LastOrDefaultOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}