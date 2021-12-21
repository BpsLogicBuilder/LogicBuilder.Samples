using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.Directives
{
    public class VariableDirectivesDescriptor
    {
        public string Field { get; set; }
        public List<DirectiveDescriptor> ConditionalDirectives { get; set; }
    }
}
