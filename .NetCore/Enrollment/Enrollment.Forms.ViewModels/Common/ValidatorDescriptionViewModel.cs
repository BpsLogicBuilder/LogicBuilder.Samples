using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class ValidatorDescriptionViewModel
    {
		public string ClassName { get; set; }
		public string FunctionName { get; set; }
		public Dictionary<string, object> Arguments { get; set; }
    }
}