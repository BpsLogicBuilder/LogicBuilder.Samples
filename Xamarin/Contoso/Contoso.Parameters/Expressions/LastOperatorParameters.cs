namespace Contoso.Parameters.Expressions
{
    public class LastOperatorParameters : FilterMethodOperatorParametersBase
    {
		public LastOperatorParameters()
		{
		}

		public LastOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}