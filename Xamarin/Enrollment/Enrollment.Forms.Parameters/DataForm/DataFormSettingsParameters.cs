using Enrollment.Forms.Parameters.Directives;
using Enrollment.Forms.Parameters.Validation;
using Enrollment.Parameters.ItemFilter;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enrollment.Forms.Parameters.DataForm
{
    public class DataFormSettingsParameters
    {
		public DataFormSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Header field on the form")]
			string title,

			[Comments("Input validation messages for each field.")]
			List<ValidationMessageParameters> validationMessages,

			[Comments("List of fields and form groups for this form.")]
			List<FormItemSettingsParameters> fieldSettings,

			[Comments("Click the Variable button and select the configured FormType enum field.")]
			FormType formType,

			[Comments("The model type for the object being edited. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("Multibindings list for the form header field - typically used in edit mode.")]
			MultiBindingParameters headerBindings = null,

			[Comments("Includes the URL's for create, read, and update.")]
			FormRequestDetailsParameters requestDetails = null,

			[Comments("Conditional directtives for each field.")]
			List<VariableDirectivesParameters> conditionalDirectives = null,

			[Comments("Multibindings list for the form header field.")]
			MultiBindingParameters subtitleBindings = null

		)
		{
			Title = title;
			RequestDetails = requestDetails;
			ValidationMessages = validationMessages.ToDictionary
			(
				vm => vm.Field,
				vm => vm.Rules ?? new List<ValidationRuleParameters>()
			);
			FieldSettings = fieldSettings;
			FormType = formType;
			ModelType = modelType;
			ConditionalDirectives = conditionalDirectives?.ToDictionary
			(
				cd => cd.Field,
				cd => cd.ConditionalDirectives ?? new List<DirectiveParameters>()
			);
			HeaderBindings = headerBindings;
			SubtitleBindings = subtitleBindings;
		}

		public string Title { get; set; }
		public FormRequestDetailsParameters RequestDetails { get; set; }
		public Dictionary<string, List<ValidationRuleParameters>> ValidationMessages { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public FormType FormType { get; set; }
		public Type ModelType { get; set; }
		public Dictionary<string, List<DirectiveParameters>> ConditionalDirectives { get; set; }
		public MultiBindingParameters HeaderBindings { get; set; }
		public MultiBindingParameters SubtitleBindings { get; set; }
	}
}