using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.TextForm
{
    public class HyperLinkSpanItemParameters : SpanItemParametersBase
    {
		public HyperLinkSpanItemParameters
		(
			[Comments("Text section of hyperlink span.")]
			string text,

			[Comments("URL section of hyperlink span.")]
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