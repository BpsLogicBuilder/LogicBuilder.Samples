using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.TextForm
{
    public class TextFormSettingsParameters
    {
		public TextFormSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Header field on the form")]
			string title,

			[Comments("List of sections on the form.  Each section includes a header and a collection of spans, links, labels or formatted labels.")]
			List<TextGroupParameters> textGroups
		)
		{
			Title = title;
			TextGroups = textGroups;
		}

		public string Title { get; set; }
		public List<TextGroupParameters> TextGroups { get; set; }
    }
}