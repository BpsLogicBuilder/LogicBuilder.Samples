using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.TextForm
{
    public class TextGroupParameters
    {
		public TextGroupParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Group header text for a group on the form")]
			string title,

			[Comments("Collection of spans, links, labels or formatted labels for this group.")]
			List<LabelItemParametersBase> labels
		)
		{
			Title = title;
			Labels = labels;
		}

		public string Title { get; set; }
		public List<LabelItemParametersBase> Labels { get; set; }
    }
}