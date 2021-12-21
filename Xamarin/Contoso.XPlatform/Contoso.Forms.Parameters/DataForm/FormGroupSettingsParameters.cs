using Contoso.Forms.Parameters.Directives;
using Contoso.Forms.Parameters.Validation;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters.DataForm
{
    public class FormGroupSettingsParameters : FormItemSettingsParameters
    {
		public FormGroupSettingsParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Source property name from the target object.")]
			string field,

			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Title for the form group.")]
			string title,

			[NameValue(AttributeNames.DEFAULTVALUE, "(Form)")]
			[Comments("Placeholder text for the for control when the form is a one-to-one form field. May need to remove this for form arrays..")]
			string validFormControlText,

			[NameValue(AttributeNames.DEFAULTVALUE, "(Invalid Form)")]
			[Comments("Placeholder text for the for control when the form is a one-to-one form field and the form is invalid. May need to remove this for form arrays..")]
			string invalidFormControlText,

			[Comments("Place holder text.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "(Property name)")]
			string placeholder,

			[Comments("The entity type for the object being edited. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("XAML template for the form group.")]
			FormGroupTemplateParameters formGroupTemplate,

			[Comments("Configuration for each field in the form group.")]
			List<FormItemSettingsParameters> fieldSettings,

			[Comments("Input validation messages for each field.")]
			List<ValidationMessageParameters> validationMessages,

			[Comments("Conditional directtives for each field.")]
			List<VariableDirectivesParameters> conditionalDirectives = null,

			[Comments("Multibindings list for the form header field.")]
			MultiBindingParameters headerBindings = null,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		)
		{
			Field = field;
			Title = title;
			ValidFormControlText = validFormControlText;
			InvalidFormControlText = invalidFormControlText;
			Placeholder = placeholder;
			ModelType = modelType;
			FormGroupTemplate = formGroupTemplate;
			FieldSettings = fieldSettings;
			ValidationMessages = validationMessages.ToDictionary
			(
				vm => vm.Field,
				vm => vm.Rules ?? new List<ValidationRuleParameters>()
			);
			ConditionalDirectives = conditionalDirectives?.ToDictionary
			(
				cd => cd.Field,
				cd => cd.ConditionalDirectives ?? new List<DirectiveParameters>()
			);
			HeaderBindings = headerBindings;
		}

		public string Field { get; set; }
		public string Title { get; set; }
		public string ValidFormControlText { get; set; }
		public string InvalidFormControlText { get; set; }
		public string Placeholder { get; set; }
		public Type ModelType { get; set; }
		public FormGroupTemplateParameters FormGroupTemplate { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public Dictionary<string, List<ValidationRuleParameters>> ValidationMessages { get; set; }
		public Dictionary<string, List<DirectiveParameters>> ConditionalDirectives { get; set; }
		public MultiBindingParameters HeaderBindings { get; set; }
	}
}