using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters
{
    public class MultiSelectTemplateParameters
    {
		public MultiSelectTemplateParameters
		(
			[Comments("XAML template for the control.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "MultiSelectTemplate")]
			string templateName,

			[Comments("Placeholder text.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "(Courses)")]
			string placeholderText,

			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Property name for the text field.")]
			string textField,

			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Property name for the value field.")]
			string valueField,

			[Comments("The model element type for the muli-select list. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("Loading text is useful when the cache has expired and the items source is being retrieved.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Loading ...")]
			string loadingIndicatorText,

			[Comments("Single object used for defining the selector lambda expression e.g. q => q.OrderBy(s => s.FullName).Select(a => new InstructorModel() {ID = a.ID, FullName = a.FullName})")]
			SelectorLambdaOperatorParameters textAndValueSelector,

			[Comments("Includes the source URL. May specify model and data types if we use the URL for multiple types.")]
			RequestDetailsParameters requestDetails,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		)
		{
			TemplateName = templateName;
			PlaceholderText = placeholderText;
			TextField = textField;
			ValueField = valueField;
			ModelType = modelType;
			LoadingIndicatorText = loadingIndicatorText;
			TextAndValueSelector = textAndValueSelector;
			RequestDetails = requestDetails;
		}

		public string TemplateName { get; set; }
		public string PlaceholderText { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
		public Type ModelType { get; set; }
		public string LoadingIndicatorText { get; set; }
		public SelectorLambdaOperatorParameters TextAndValueSelector { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}