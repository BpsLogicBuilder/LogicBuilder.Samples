using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Bindings
{
    public class TextItemBindingParameters : ItemBindingParameters
	{
		public TextItemBindingParameters
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

			[Comments("Useful when we need to one template in edit mode and a different one in add mode.  Holds the XAML template name for the field.")]
			TextFieldTemplateParameters textTemplate,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = null
		) : base(name, property, title, stringFormat, fieldTypeSource)
		{
			TextTemplate = textTemplate;
		}

		public TextFieldTemplateParameters TextTemplate { get; set; }
	}
}
