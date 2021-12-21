using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class ThenByOperatorDescriptor : SelectorMethodOperatorDescriptorBase
    {
		public ListSortDirection SortDirection { get; set; }
    }
}