using System.Collections.Generic;

namespace Contoso.Parameters.Expressions
{
    abstract public class FilterMethodOperatorParametersBase : IExpressionParameter
    {
		public FilterMethodOperatorParametersBase()
		{
		}

		public FilterMethodOperatorParametersBase(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null)
		{
			SourceOperand = sourceOperand;
			FilterBody = filterBody;
			FilterParameterName = filterParameterName;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public IExpressionParameter FilterBody { get; set; }
		public string FilterParameterName { get; set; }
    }
}