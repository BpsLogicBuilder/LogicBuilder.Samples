namespace Contoso.Parameters.Expressions
{
    public class FirstOrDefaultOperatorParameters : FilterMethodOperatorParametersBase
    {
		public FirstOrDefaultOperatorParameters()
		{
		}

		public FirstOrDefaultOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}