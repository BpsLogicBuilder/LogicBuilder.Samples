namespace Contoso.Parameters.Expressions
{
    public class SingleOperatorParameters : FilterMethodOperatorParametersBase
    {
		public SingleOperatorParameters()
		{
		}

		public SingleOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}