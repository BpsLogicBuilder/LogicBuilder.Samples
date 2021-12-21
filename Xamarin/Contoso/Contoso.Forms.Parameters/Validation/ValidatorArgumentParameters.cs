using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidatorArgumentParameters
    {
		public ValidatorArgumentParameters
		(
			[Comments("The argument's name.")]
			[NameValue(AttributeNames.USEFOREQUALITY, "true")]
			[NameValue(AttributeNames.USEFORHASHCODE, "true")]
			string name,

			[Comments("The argument's value.")]
			[NameValue(AttributeNames.USEFOREQUALITY, "false")]
			[NameValue(AttributeNames.USEFORHASHCODE, "false")]
			object value,

			[Comments("The argumet type. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.  Use the full name (e.g. System.Int32) for literals or core platform types.")]
			Type type
		)
		{
			Name = name;
			Value = value;
			Type = type;
		}

		public string Name { get; set; }
		public object Value { get; set; }
		public Type Type { get; set; }
    }
}