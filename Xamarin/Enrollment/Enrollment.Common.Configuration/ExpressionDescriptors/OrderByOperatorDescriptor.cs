using LogicBuilder.Expressions.Utils.Strutures;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class OrderByOperatorDescriptor : SelectorMethodOperatorDescriptorBase
    {
		public ListSortDirection SortDirection { get; set; }
    }
}