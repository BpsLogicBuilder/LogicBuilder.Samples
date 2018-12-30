using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class VariableDirectivesViewModel
    {
		public string Field { get; set; }
		public List<DirectiveViewModel> ConditionalDirectives { get; set; }
    }
}