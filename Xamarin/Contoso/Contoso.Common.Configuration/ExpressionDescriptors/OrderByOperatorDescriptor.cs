using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class OrderByOperatorDescriptor : SelectorMethodOperatorDescriptorBase
    {
		public ListSortDirection SortDirection { get; set; }
    }
}