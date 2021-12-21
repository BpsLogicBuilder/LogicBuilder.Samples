using LogicBuilder.Attributes;

namespace Enrollment.Forms.Parameters.TextForm
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