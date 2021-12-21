using Contoso.Forms.Parameters.Directives;
using Contoso.Forms.Parameters.Validation;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters.DataForm
{
    public class FormGroupArraySettingsParameters : FormItemSettingsParameters
    {
		public FormGroupArraySettingsParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Source property name from the target object.")]
			string field,

			[Comments("Update listElementTypeSource first. Usually just a list of one item - the primary key. Additional fields apply when the primary key is a composite key.")]
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "listElementTypeSource")]
			List<string> keyFields,

			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Title for the form group.")]
			string title,

			[NameValue(AttributeNames.DEFAULTVALUE, "(Form Collection)")]
			[Comments("e.g. (Addresses). Placeholder text for the for multi-form control on the parent form.")]
			string placeholder,

			[Comments("e.g. T. The element type for the list being edited. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("e.g. ICollection<T>. The type for the list being edited. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type type,

			[NameValue(AttributeNames.DEFAULTVALUE, "(Form)")]
			[Comments("Placeholder text for the for control when the form is a one-to-one form field.")]
			string validFormControlText,

			[NameValue(AttributeNames.DEFAULTVALUE, "(Invalid Form)")]
			[Comments("Placeholder text for the for control when the form is a one-to-one form field and the form is invalid.")]
			string invalidFormControlText,

			[Comments("Template and property bindings to be displayed for each item in the list.")]
			FormsCollectionDisplayTemplateParameters formsCollectionDisplayTemplate,

			[Comments("XAML template for the form group.")]
			FormGroupTemplateParameters formGroupTemplate,

			[Comments("Configuration for each field in one of the array's form groups.")]
			List<FormItemSettingsParameters> fieldSettings,

			[Comments("Input validation messages for each field.")]
			List<ValidationMessageParameters> validationMessages,

			[Comments("Conditional directtives for each field.")]
			List<VariableDirectivesParameters> conditionalDirectives = null,

			[Comments("Multibindings list for the form header field.")]
			MultiBindingParameters headerBindings = null,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities",

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string listElementTypeSource = "Contoso.Domain.Entities"
		)
		{
			Field = field;
			Title = title;
			Placeholder = placeholder;
			ModelType = modelType;
			Type = type;
			ValidFormControlText = validFormControlText;
			InvalidFormControlText = invalidFormControlText;
			KeyFields = keyFields;
			FormsCollectionDisplayTemplate = formsCollectionDisplayTemplate;
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
		public string Placeholder { get; set; }
		public Type ModelType { get; set; }
		public Type Type { get; set; }
		public string ValidFormControlText { get; set; }
		public string InvalidFormControlText { get; set; }
		public List<string> KeyFields { get; set; }
		public FormsCollectionDisplayTemplateParameters FormsCollectionDisplayTemplate { get; set; }
		public FormGroupTemplateParameters FormGroupTemplate { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public Dictionary<string, List<ValidationRuleParameters>> ValidationMessages { get; set; }
		public Dictionary<string, List<DirectiveParameters>> ConditionalDirectives { get; set; }
		public MultiBindingParameters HeaderBindings { get; set; }
	}
}