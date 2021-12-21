using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidatorDefinitionDescriptor
    {
        public string ClassName { get; set; }
        public string FunctionName { get; set; }
        public Dictionary<string, ValidatorArgumentDescriptor> Arguments { get; set; }
    }
}
