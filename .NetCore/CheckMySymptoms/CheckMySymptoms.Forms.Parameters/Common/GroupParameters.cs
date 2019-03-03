using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class GroupParameters
    {
        public GroupParameters()
        {
        }

        public GroupParameters
        (
            [Comments("Update modelType first. Property name from the target object. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Domain("asc,desc")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string dir,

            List<AggregateDefinitionParameters> aggregates,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "CheckMySymptoms.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            Field = field;
            Dir = dir;
            Aggregates = aggregates;
        }

        public string Field { get; set; }
        public string Dir { get; set; }
        public List<AggregateDefinitionParameters> Aggregates { get; set; }
    }
}