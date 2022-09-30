using Contoso.Forms.Parameters.Bindings;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters.SearchForm
{
	public class SearchFormSettingsParameters
    {
		public SearchFormSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Header field on the form")]
			string title,

			[Comments("The element type for a list item. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("Loading text may be useful.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Lodding ...")]
			string loadingIndicatorText,

			[Comments("XAML template name for the collection view item template.")]
			[Domain("HeaderTextDetailTemplate, TextDetailTemplate")]
			[NameValue(AttributeNames.DEFAULTVALUE, "HeaderTextDetailTemplate")]
			string itemTemplateName,

			[Comments("Placeholder text for the search bar.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Filter")]
			string filterPlaceholder,

            [Comments("Flow for dynamically creating the paging (where/orderby/skip/take) selector e.g. paging_selector_courses.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "paging_selector_models")]
            string createPagingSelectorFlowName,

            [Comments("Defines which fields of the model type which bind to the named template fields (e.g. Header, Text, Detail).")]
			List<ItemBindingParameters> bindings,

			[Comments("Defines API URL for the list data. May specify model and data types if we use the URL for multiple types.")]
			RequestDetailsParameters requestDetails
		)
		{
			Title = title;
			ModelType = modelType;
			LoadingIndicatorText = loadingIndicatorText;
			ItemTemplateName = itemTemplateName;
			FilterPlaceholder = filterPlaceholder;
			CreatePagingSelectorFlowName = createPagingSelectorFlowName;
			Bindings = bindings.ToDictionary(cvib => cvib.Name);
			RequestDetails = requestDetails;
		}

		public string Title { get; set; }
		public Type ModelType { get; set; }
		public string LoadingIndicatorText { get; set; }
		public string ItemTemplateName { get; set; }
		public string FilterPlaceholder { get; set; }
        public string CreatePagingSelectorFlowName { get; set; }
        public Dictionary<string, ItemBindingParameters> Bindings { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}