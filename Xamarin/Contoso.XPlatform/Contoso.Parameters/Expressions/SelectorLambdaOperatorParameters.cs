using System.Collections.Generic;
using System;

namespace Contoso.Parameters.Expressions
{
    public class SelectorLambdaOperatorParameters : IExpressionParameter
    {
		public SelectorLambdaOperatorParameters()
		{
		}

		public SelectorLambdaOperatorParameters(IExpressionParameter selector, Type sourceElementType, string parameterName, Type bodyType = null)
		{
			Selector = selector;
			SourceElementType = sourceElementType;
			ParameterName = parameterName;
			BodyType = bodyType;
		}

		public IExpressionParameter Selector { get; set; }
		public Type SourceElementType { get; set; }
		public Type BodyType { get; set; }
		public string ParameterName { get; set; }
    }
}