using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class VariableDirectivesView
    {
		public string Field { get; set; }
		public List<DirectiveView> ConditionalDirectives { get; set; }
    }
}