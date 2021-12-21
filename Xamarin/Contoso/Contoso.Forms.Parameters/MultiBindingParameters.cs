using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters
{
    public class MultiBindingParameters
    {
		public MultiBindingParameters
		(
			[Comments("Specify a format for the multi binding e.g. 'Value: {0:F2} {1}'")]
			[NameValue(AttributeNames.DEFAULTVALUE, "{0}")]
			string stringFormat,

			[Comments("The list of fields to be bound in the multibinding.")]
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			List<string> fields,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = null
		)
		{
			StringFormat = stringFormat;
			Fields = fields;
		}

		public string StringFormat { get; set; }
		public List<string> Fields { get; set; }
    }
}