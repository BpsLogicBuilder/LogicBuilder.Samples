using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.TextForm
{
    public class HyperLinkLabelItemParameters : LabelItemParametersBase
    {
		public HyperLinkLabelItemParameters
		(
			[Comments("Text section of hyperlink label.")]
			string text,

			[Comments("URL section of hyperlink label.")]
			string url
		)
		{
			Text = text;
			Url = url;
		}

		public string Text { get; set; }
		public string Url { get; set; }
    }
}