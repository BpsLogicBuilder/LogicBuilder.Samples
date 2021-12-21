using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidationMessageDescriptor
    {
        public string Field { get; set; }
        public List<ValidationRuleDescriptor> Rules { get; set; }
    }
}
