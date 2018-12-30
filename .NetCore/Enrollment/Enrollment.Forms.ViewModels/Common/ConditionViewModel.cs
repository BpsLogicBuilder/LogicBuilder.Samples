using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class ConditionViewModel
    {
		public string Operator { get; set; }
		public string LeftVariable { get; set; }
		public string RightVariable { get; set; }
		public object Value { get; set; }
    }
}