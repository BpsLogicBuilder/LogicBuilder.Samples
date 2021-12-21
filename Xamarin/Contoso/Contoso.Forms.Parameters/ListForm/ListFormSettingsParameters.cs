using Contoso.Forms.Parameters.Bindings;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters.ListForm
{
    public class ListFormSettingsParameters
    {
		public ListFormSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Header field on the form")]
			string title,

			[Comments("The element type for a list item. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("Loading text may be useful.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Loading ...")]
			string loadingIndicatorText,

			[Comments("XAML template name for the collection view item template.")]
			[Domain("HeaderTextDetailTemplate, TextDetailTemplate")]
			[NameValue(AttributeNames.DEFAULTVALUE, "HeaderTextDetailTemplate")]
			string itemTemplateName,

			[Comments("Defines which of the model type fields bind to the named template fields (e.g. Header, Text, Detail).")]
			List<ItemBindingParameters> bindings,

			[Comments("Defines the LINQ query for retrieving the list.")]
			SelectorLambdaOperatorParameters fieldsSelector,

			[Comments("Defines API URL for the list data. May specify model and data types if we use the URL for multiple types.")]
			RequestDetailsParameters requestDetails
		)
		{
			Title = title;
			ModelType = modelType;
			LoadingIndicatorText = loadingIndicatorText;
			ItemTemplateName = itemTemplateName;
			Bindings = bindings.ToDictionary(b => b.Name);
			FieldsSelector = fieldsSelector;
			RequestDetails = requestDetails;
		}

		public string Title { get; set; }
		public Type ModelType { get; set; }
		public string LoadingIndicatorText { get; set; }
		public string ItemTemplateName { get; set; }
		public Dictionary<string, ItemBindingParameters> Bindings { get; set; }
		public SelectorLambdaOperatorParameters FieldsSelector { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}