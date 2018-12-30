using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class ConditionGroupViewModel
    {
		public string Logic { get; set; }
		public List<ConditionViewModel> Conditions { get; set; }
		public List<ConditionGroupViewModel> ConditionGroups { get; set; }
    }
}