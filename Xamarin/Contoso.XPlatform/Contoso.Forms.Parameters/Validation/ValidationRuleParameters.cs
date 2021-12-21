using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidationRuleParameters
    {
		public ValidationRuleParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "RequiredRule")]
			[Comments("The validation class")]
			[Domain("IsLengthValidRule,IsMatchRule,IsPatternMatchRule,IsValidEmailRule,IsValidPasswordRule,IsValueTrueRule,MustBeIntegerRule,MustBeNumberRule,MustBePositiveNumberRule,RangeRule,RequiredRule,AtLeastOneRequiredRule")]
			string className,

			[Comments("The validtion message")]
			[NameValue(AttributeNames.DEFAULTVALUE, "(Property) is required.")]
			string message
		)
		{
			ClassName = className;
			Message = message;
		}

		public string ClassName { get; set; }
		public string Message { get; set; }
    }
}