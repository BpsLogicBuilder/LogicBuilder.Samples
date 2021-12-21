using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Parameters.ItemFilter
{
    public class ItemFilterGroupParameters : ItemFilterParametersBase
    {
		public ItemFilterGroupParameters
		(
			[Domain("and,or")]
			[ParameterEditorControl(ParameterControlType.DropDown)]
			[Comments("and,or")]
			[NameValue(AttributeNames.DEFAULTVALUE, "and")]
			string logic,

			[Comments("List of filters - maximum of 2.  For additional filters use add a filter group instead of a filter.")]
			ICollection<ItemFilterParametersBase> filters
		)
		{
			Logic = logic;
			Filters = filters;
		}

		public string Logic { get; set; }
		public ICollection<ItemFilterParametersBase> Filters { get; set; }
    }
}