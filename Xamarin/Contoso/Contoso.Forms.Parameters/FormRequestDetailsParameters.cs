using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters
{
    public class FormRequestDetailsParameters
    {
		public FormRequestDetailsParameters
		(
			[Comments("API end point to get the entity.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "api/Entity/GetEntity")]
			string getUrl,

			[Comments("API end point to add the entity.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "api/Student/Save")]
			string addUrl,

			[Comments("API end point to update the entity.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "api/Student/Save")]
			string updateUrl,

			[Comments("API end point to update the entity.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "api/Student/Delete")]
			string deleteUrl,

			[Comments("The model type for the object being requested. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("The data type for the object being requested. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type dataType,

			[Comments("Defines the filter for the single object being edited - only applicable when the edit type is update.")]
			FilterLambdaOperatorParameters filter = null,

			[Comments("Defines and navigation properties to include in the edit model")]
			SelectExpandDefinitionParameters selectExpandDefinition = null
		)
		{
			GetUrl = getUrl;
			AddUrl = addUrl;
			UpdateUrl = updateUrl;
			DeleteUrl = deleteUrl;
			ModelType = modelType;
			DataType = dataType;
			Filter = filter;
			SelectExpandDefinition = selectExpandDefinition;
		}

		public string GetUrl { get; set; }
		public string AddUrl { get; set; }
		public string UpdateUrl { get; set; }
		public string DeleteUrl { get; set; }
		public Type ModelType { get; set; }
		public Type DataType { get; set; }
		public FilterLambdaOperatorParameters Filter { get; set; }
		public SelectExpandDefinitionParameters SelectExpandDefinition { get; set; }
    }
}