using Contoso.Forms.Parameters.Validation;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.DataForm
{
    public class FormControlSettingsParameters : FormItemSettingsParameters
    {
		public FormControlSettingsParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. This property being edited.")]
			string field,

			[Comments("Label for the field.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			string title,

			[Comments("Place holder text.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "(Title) required")]
			string placeholder,

			[Comments("String format - useful for binding decimals.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "{0}")]
			string stringFormat,

			[Comments("The type for the field being edited. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.  Use the full name (e.g. System.Int32) for literals or core platform types.")]
			Type type,

			[Comments("Defines the field's default value, validation functions (and arguments for the validator where necessary).")]
			FieldValidationSettingsParameters validationSetting = null,

			[Comments("Holds the XAML template name for the field.")]
			TextFieldTemplateParameters textTemplate = null,

			[Comments("Holds the XAML template name for the field plus additional drop-down related properties (textField, valueField, request details etc.).")]
			DropDownTemplateParameters dropDownTemplate = null,

			[Comments("Useful when we need to one template in edit mode and a different one in add mode.  Holds the XAML template name for the field.")]
			TextFieldTemplateParameters updateOnlytextTemplate = null,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		)
		{
			Field = field;
			Title = title;
			Placeholder = placeholder;
			StringFormat = stringFormat;
			Type = type;
			ValidationSetting = validationSetting;
			TextTemplate = textTemplate;
			DropDownTemplate = dropDownTemplate;
			UpdateOnlyTextTemplate = updateOnlytextTemplate;
		}

		public string Field { get; set; }
		public string Title { get; set; }
		public string Placeholder { get; set; }
		public string StringFormat { get; set; }
		public Type Type { get; set; }
		public FieldValidationSettingsParameters ValidationSetting { get; set; }
		public TextFieldTemplateParameters TextTemplate { get; set; }
		public DropDownTemplateParameters DropDownTemplate { get; set; }
		public TextFieldTemplateParameters UpdateOnlyTextTemplate { get; set; }
	}
}