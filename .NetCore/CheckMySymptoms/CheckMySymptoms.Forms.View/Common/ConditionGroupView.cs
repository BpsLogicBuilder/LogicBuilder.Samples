using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class ConditionGroupView
    {
		public string Logic { get; set; }
		public List<ConditionView> Conditions { get; set; }
		public List<ConditionGroupView> ConditionGroups { get; set; }
    }
}