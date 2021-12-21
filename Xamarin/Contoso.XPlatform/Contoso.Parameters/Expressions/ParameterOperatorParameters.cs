using System.Collections.Generic;

namespace Contoso.Parameters.Expressions
{
    public class ParameterOperatorParameters : IExpressionParameter
    {
		public ParameterOperatorParameters()
		{
		}

		public ParameterOperatorParameters(string parameterName)
		{
			ParameterName = parameterName;
		}

		public string ParameterName { get; set; }
    }
}