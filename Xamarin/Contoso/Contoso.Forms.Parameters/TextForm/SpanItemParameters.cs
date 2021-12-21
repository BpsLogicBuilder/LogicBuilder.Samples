using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.TextForm
{
    public class SpanItemParameters : SpanItemParametersBase
    {
		public SpanItemParameters
		(
			[Comments("Span text.")]
			string text
		)
		{
			Text = text;
		}

		public string Text { get; set; }
    }
}