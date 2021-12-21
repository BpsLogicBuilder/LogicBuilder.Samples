using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Bindings
{
    public class MultiSelectItemBindingParameters : ItemBindingParameters
	{
		public MultiSelectItemBindingParameters
		(
			[Comments("The section of the item template we're binding the property to.")]
			[Domain("Header,Text,Detail")]
			[NameValue(AttributeNames.USEFOREQUALITY, "true")]
			[NameValue(AttributeNames.USEFORHASHCODE, "true")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Header")]
			string name,

			[Comments("Update fieldTypeSource first. The property to bind to the name section.")]
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			string property,

			[Comments("Label for the field.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			string title,

			[Comments("Specify a format for the binding e.g. 'Value: {0:F2}'")]
			[NameValue(AttributeNames.DEFAULTVALUE, "{0}")]
			string stringFormat,

			[Comments("Update listElementTypeSource first. Usually just a list of one item - the primary key. Additional fields apply when the primary key is a composite key.")]
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "listElementTypeSource")]
			List<string> keyFields,

			[Comments("Holds the XAML template name for the field plus additional multi-select related properties (textField, valueField, request details etc.).")]
			MultiSelectTemplateParameters multiSelectTemplate,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = null,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string listElementTypeSource = "Enrollment.Domain.Entities"
		) : base(name, property, title, stringFormat, fieldTypeSource)
		{
			KeyFields = keyFields;
			MultiSelectTemplate = multiSelectTemplate;
		}

		public List<string> KeyFields { get; set; }
		public MultiSelectTemplateParameters MultiSelectTemplate { get; set; }
	}
}
