using Contoso.Forms.Parameters.Bindings;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters
{
    public class FormsCollectionDisplayTemplateParameters
    {
		public FormsCollectionDisplayTemplateParameters
		(
			[Comments("XAML template name for the collection view item template.")]
			[Domain("HeaderTextDetailTemplate, TextDetailTemplate")]
			[NameValue(AttributeNames.DEFAULTVALUE, "HeaderTextDetailTemplate")]
			string templateName,

			[Comments("Defines which of the model type fields bind to the named template fields (e.g. Header, Text, Detail).")]
			List<ItemBindingParameters> bindings
		)
		{
			TemplateName = templateName;
			Bindings = bindings.ToDictionary(cvib => cvib.Name);
		}

		public string TemplateName { get; set; }
		public Dictionary<string, ItemBindingParameters> Bindings { get; set; }
    }
}