namespace Contoso.Parameters.Expressions
{
    public class FirstOperatorParameters : FilterMethodOperatorParametersBase
    {
		public FirstOperatorParameters()
		{
		}

		public FirstOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}