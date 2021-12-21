using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandItemParameters
    {
        public SelectExpandItemParameters()
        {
        }

        public SelectExpandItemParameters
        (
            [Comments("Update fieldTypeSource first. This is the navigation property name.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
            string memberName,

            [Comments("Used to filter the navigation property when it is a collection.")]
            SelectExpandItemFilterParameters filter = null,

            [Comments("Used to apply sort, skip and take to the navigation property when it is a collection.")]
            SelectExpandItemQueryFunctionParameters queryFunction = null,

            [Comments("Update navigationProperyType first. This is a list of fields to select when a subset of fields is required.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "navigationProperyType")]
            List<string> selects = null,

            [Comments("List of navigation properties to expand.")]
            List<SelectExpandItemParameters> expandedItems = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string fieldTypeSource = "Enrollment.Domain.Entities",

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string navigationProperyType = "Enrollment.Domain.Entities"
        )
        {
            MemberName = memberName;
            Filter = filter;
            QueryFunction = queryFunction;
            Selects = selects ?? new List<string>();
            ExpandedItems = expandedItems ?? new List<SelectExpandItemParameters>();
        }

        public string MemberName { get; set; }
        public SelectExpandItemFilterParameters Filter { get; set; }
        public SelectExpandItemQueryFunctionParameters QueryFunction { get; set; }
        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemParameters> ExpandedItems { get; set; } = new List<SelectExpandItemParameters>();
    }
}
