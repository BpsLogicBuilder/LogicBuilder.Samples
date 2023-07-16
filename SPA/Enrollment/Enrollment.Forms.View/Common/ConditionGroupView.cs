using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class ConditionGroupView
    {
        public string Logic { get; set; }
        public List<ConditionView> Conditions { get; set; }
        public List<ConditionGroupView> ConditionGroups { get; set; }
    }
}