using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters
{
    public class DropDownTemplateParameters
    {
		public DropDownTemplateParameters
		(
			[Comments("XAML template for the picker.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "PickerTemplate")]
			string templateName,

			[Comments("Title text.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Select (Entity)")]
			string titleText,

			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Property name for the text field.")]
			string textField,

			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Property name for the value field.")]
			string valueField,

			[Comments("Loading text is useful when the cache has expired and the items source is being retrieved.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Loading ...")]
			string loadingIndicatorText,

			[Comments("Single object used for defining the selector lambda expression e.g. q => q.OrderBy(s => s.FullName).Select(a => new InstructorModel() {ID = a.ID, FullName = a.FullName})")]
			SelectorLambdaOperatorParameters textAndValueSelector,

			[Comments("Includes the source URL. May specify model and data types if we use the URL for multiple types.")]
			RequestDetailsParameters requestDetails,

			[Comments("Used the dropdown or multiselect items change conditionally at runtime.  Add a flow module to define a new selector (SelectorLambdaOperatorParameters) at run time.  Set reloadItemsFlowName to the flow name.")]
			string reloadItemsFlowName = null,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		)
		{
			TemplateName = templateName;
			TitleText = titleText;
			TextField = textField;
			ValueField = valueField;
			LoadingIndicatorText = loadingIndicatorText;
			TextAndValueSelector = textAndValueSelector;
			RequestDetails = requestDetails;
			ReloadItemsFlowName = reloadItemsFlowName;
		}

		public string TemplateName { get; set; }
		public string TitleText { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
		public string LoadingIndicatorText { get; set; }
		public SelectorLambdaOperatorParameters TextAndValueSelector { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
		public string ReloadItemsFlowName { get; set; }
	}
}