namespace Contoso.Parameters.Expressions
{
    public class AnyOperatorParameters : FilterMethodOperatorParametersBase
    {
		public AnyOperatorParameters()
		{
		}

		public AnyOperatorParameters(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}