using Enrollment.Common.Configuration.ExpressionDescriptors;

namespace Enrollment.Forms.Configuration.Directives
{
    public class DirectiveDescriptor
    {
        public DirectiveDefinitionDescriptor Definition { get; set; }
        public FilterLambdaOperatorDescriptor Condition { get; set; }
    }
}
