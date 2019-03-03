using LogicBuilder.Attributes;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class AggregateDefinitionParameters
    {
        public AggregateDefinitionParameters()
        {
        }

        public AggregateDefinitionParameters
        (
            [Comments("Update modelType first. Property name for the aggregate field. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Domain("average,count,max,min,sum")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string aggregate,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "CheckMySymptoms.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            Field = field;
            Aggregate = aggregate;
        }

        public string Field { get; set; }
        public string Aggregate { get; set; }
    }
}