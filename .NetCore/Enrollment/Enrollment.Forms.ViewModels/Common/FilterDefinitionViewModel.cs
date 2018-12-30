using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class FilterDefinitionViewModel
    {
		public string Field { get; set; }
		public string Operator { get; set; }
		public object Value { get; set; }
		public bool? IgnoreCase { get; set; }
		public string ValueSourceMember { get; set; }
    }
}