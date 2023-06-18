using LogicBuilder.Expressions.Utils.Strutures;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class ThenByOperatorDescriptor : SelectorMethodOperatorDescriptorBase
    {
        public ListSortDirection SortDirection { get; set; }
    }
}