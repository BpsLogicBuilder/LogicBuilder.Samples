using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.Validation
{
    public class ValidatorDefinitionDescriptor
    {
        public string ClassName { get; set; }
        public string FunctionName { get; set; }
        public Dictionary<string, ValidatorArgumentDescriptor> Arguments { get; set; }
    }
}
