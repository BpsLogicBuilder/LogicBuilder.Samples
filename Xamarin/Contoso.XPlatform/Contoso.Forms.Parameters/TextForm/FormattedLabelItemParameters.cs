using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.TextForm
{
    public class FormattedLabelItemParameters : LabelItemParametersBase
    {
		public FormattedLabelItemParameters
		(
			[Comments("List of text and hyperlink items which combine to form the formatted label.")]
			List<SpanItemParametersBase> items
		)
		{
			Items = items;
		}

		public List<SpanItemParametersBase> Items { get; set; }
    }
}