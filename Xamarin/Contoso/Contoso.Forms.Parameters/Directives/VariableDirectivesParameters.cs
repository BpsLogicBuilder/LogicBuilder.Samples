using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Directives
{
    public class VariableDirectivesParameters
    {
		public VariableDirectivesParameters
		(
			[Comments("Update fieldTypeSource first. The property to which the directive applies.")]
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			string field,

			[Comments("List of object which define the directive including functions/expressions for enabling/disabling the directive.")]
			List<DirectiveParameters> conditionalDirectives,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = null
		)
		{
			Field = field;
			ConditionalDirectives = conditionalDirectives;
		}

		public string Field { get; set; }
		public List<DirectiveParameters> ConditionalDirectives { get; set; }
    }
}