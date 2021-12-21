using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Bindings
{
    public class DropDownItemBindingParameters : ItemBindingParameters
	{
		public DropDownItemBindingParameters
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

			[Comments("Holds the XAML template name for the field plus additional drop-down related properties (textField, valueField, request details etc.).")]
			DropDownTemplateParameters dropDownTemplate,

			[Comments("Set to true if the selector depends on another property.  The DropDownTemplate.ReloadItemsFlowName must also be set to define the new selector.")]
			bool requiresReload,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = null
		) : base(name, property, title, stringFormat, fieldTypeSource)
		{
			DropDownTemplate = dropDownTemplate;
			RequiresReload = requiresReload;
		}

		public DropDownTemplateParameters DropDownTemplate { get; set; }
		public bool RequiresReload { get; set; }
	}
}
