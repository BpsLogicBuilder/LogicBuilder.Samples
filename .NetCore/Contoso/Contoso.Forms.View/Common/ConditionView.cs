using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class ConditionView
    {
		public string Operator { get; set; }
		public string LeftVariable { get; set; }
		public string RightVariable { get; set; }
		public object Value { get; set; }
    }
}