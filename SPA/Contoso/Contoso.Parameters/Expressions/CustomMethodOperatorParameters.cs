using System.Reflection;

namespace Contoso.Parameters.Expressions
{
    public class CustomMethodOperatorParameters : IExpressionParameter
    {
		public CustomMethodOperatorParameters()
		{
		}

		public CustomMethodOperatorParameters(MethodInfo methodInfo, IExpressionParameter[] args)
		{
			MethodInfo = methodInfo;
			Args = args;
		}

		public MethodInfo MethodInfo { get; set; }
		public IExpressionParameter[] Args { get; set; }
    }
}