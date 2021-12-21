using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.Directives
{
    public class DirectiveDefinitionDescriptor
    {
        public string ClassName { get; set; }
        public string FunctionName { get; set; }
        public Dictionary<string, DirectiveArgumentDescriptor> Arguments { get; set; }
    }
}
