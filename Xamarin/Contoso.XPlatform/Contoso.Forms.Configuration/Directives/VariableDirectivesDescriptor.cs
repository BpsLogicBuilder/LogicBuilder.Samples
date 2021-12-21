using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Directives
{
    public class VariableDirectivesDescriptor
    {
        public string Field { get; set; }
        public List<DirectiveDescriptor> ConditionalDirectives { get; set; }
    }
}
