using Contoso.Common.Configuration.ExpressionDescriptors;

namespace Contoso.Forms.Configuration.Directives
{
    public class DirectiveDescriptor
    {
        public DirectiveDefinitionDescriptor Definition { get; set; }
        public FilterLambdaOperatorDescriptor Condition { get; set; }
    }
}
