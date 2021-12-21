namespace Contoso.Parameters.Expressions
{
    public class CountOperatorParameters : FilterMethodOperatorParametersBase
    {
		public CountOperatorParameters()
		{
		}

		public CountOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}