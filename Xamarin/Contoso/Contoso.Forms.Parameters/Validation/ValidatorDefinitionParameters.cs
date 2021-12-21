using LogicBuilder.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidatorDefinitionParameters
    {
		public ValidatorDefinitionParameters
		(
			[Comments("Validation class name.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "RequiredRule")]
			[Domain("IsLengthValidRule,IsMatchRule,IsPatternMatchRule,IsValidEmailRule,IsValidPasswordRule,IsValueTrueRule,MustBeIntegerRule,MustBeNumberRule,MustBePositiveNumberRule,RangeRule,RequiredRule,AtLeastOneRequiredRule")]
			string className,

			[Comments("Function to call on the validation class.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Check")]
			string functionName,

			[Comments("Where applicable, add arguments for the validator function e.g. min, max vallues.")]
			List<ValidatorArgumentParameters> arguments = null
		)
		{
			ClassName = className;
			FunctionName = functionName;
			Arguments = arguments?.ToDictionary(arg => arg.Name);
		}

		public string ClassName { get; set; }
		public string FunctionName { get; set; }
		public Dictionary<string, ValidatorArgumentParameters> Arguments { get; set; }
    }
}