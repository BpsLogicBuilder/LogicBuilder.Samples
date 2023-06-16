using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class ValidatorDescriptionView
    {
		public string ClassName { get; set; }
		public string FunctionName { get; set; }
		public Dictionary<string, object> Arguments { get; set; }
    }
}