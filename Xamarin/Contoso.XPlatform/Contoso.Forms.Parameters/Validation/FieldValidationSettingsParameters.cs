using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Validation
{
    public class FieldValidationSettingsParameters
    {
		public FieldValidationSettingsParameters
		(
			[Comments("Default value for the form control.")]
			object defaultValue = null,

			[Comments("Confifuration for validation classes, functions (and arguments for the validator where necessary).")]
			List<ValidatorDefinitionParameters> validators = null
		)
		{
			DefaultValue = defaultValue;
			Validators = validators;
		}

		public object DefaultValue { get; set; }
		public List<ValidatorDefinitionParameters> Validators { get; set; }
    }
}