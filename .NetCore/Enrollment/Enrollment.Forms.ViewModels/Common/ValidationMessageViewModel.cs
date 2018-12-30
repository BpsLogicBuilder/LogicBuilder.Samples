using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class ValidationMessageViewModel
    {
		public string Field { get; set; }
		public Dictionary<string, string> Methods { get; set; }
    }
}