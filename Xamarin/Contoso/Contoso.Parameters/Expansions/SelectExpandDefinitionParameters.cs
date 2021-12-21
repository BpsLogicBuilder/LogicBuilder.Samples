using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandDefinitionParameters
    {
        public SelectExpandDefinitionParameters()
        {
        }

        public SelectExpandDefinitionParameters
        (
            [Comments("Update fieldTypeSource first. List of fields to select when a subset of fields is required.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
            List<string> selects,

            [Comments("List of navigation properties to expand.")]
            List<SelectExpandItemParameters> expandedItems,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string fieldTypeSource = "Enrollment.Domain.Entities"
        )
        {
            Selects = selects;
            ExpandedItems = expandedItems;
        }

        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemParameters> ExpandedItems { get; set; } = new List<SelectExpandItemParameters>();
    }
}
