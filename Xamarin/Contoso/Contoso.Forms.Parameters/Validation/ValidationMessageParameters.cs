using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidationMessageParameters
    {
		public ValidationMessageParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. This property being validated.")]
			[NameValue(AttributeNames.USEFOREQUALITY, "true")]
			[NameValue(AttributeNames.USEFORHASHCODE, "true")]
			string field,

			[Comments("List of validation classes and messages to be used by the validatable view model.")]
			List<ValidationRuleParameters> rules,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		)
		{
			Field = field;
			Rules = rules;
		}

		public string Field { get; set; }
		public List<ValidationRuleParameters> Rules { get; set; }
    }
}