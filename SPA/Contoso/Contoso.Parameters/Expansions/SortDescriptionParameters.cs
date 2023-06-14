using LogicBuilder.Attributes;
using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.Parameters.Expansions
{
    public class SortDescriptionParameters
    {
        public SortDescriptionParameters()
        {

        }

        public SortDescriptionParameters
        (
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
            [Comments("Update fieldTypeSource first. This property to sort by.")]
            string propertyName,

            [Comments("Click the variable button and select th configured ListSortDirection.")]
            ListSortDirection order,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string fieldTypeSource = "Contoso.Domain.Entities"
        )
        {
            this.PropertyName = propertyName;
            this.SortDirection = order;
        }

        public string PropertyName { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}
