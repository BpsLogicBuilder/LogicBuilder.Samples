using LogicBuilder.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveDefinitionParameters
    {
		public DirectiveDefinitionParameters
		(
			[Comments("Class name for the directive.")]
			[Domain("DisableIf,HideIf,ValidateIf")]
			[NameValue(AttributeNames.DEFAULTVALUE, "HideIf")]
			string className,

			[Comments("Function name.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Check")]
			[Domain("Check")]
			string functionName,

			[Comments("Where applicable, add arguments for the directive evaluation function.")]
			List<DirectiveArgumentParameters> arguments = null
		)
		{
			ClassName = className;
			FunctionName = functionName;
			Arguments = arguments?.ToDictionary(arg => arg.Name);
		}

		public string ClassName { get; set; }
		public string FunctionName { get; set; }
		public Dictionary<string, DirectiveArgumentParameters> Arguments { get; set; }
    }
}