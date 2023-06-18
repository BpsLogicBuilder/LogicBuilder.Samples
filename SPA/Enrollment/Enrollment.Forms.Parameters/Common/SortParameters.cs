using LogicBuilder.Attributes;

namespace Enrollment.Forms.Parameters.Common
{
    public class SortParameters
    {
        public SortParameters()
        {
        }

        public SortParameters
        (
            [Comments("Update modelType first. Property name from the target object. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Domain("asc,desc")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string dir,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            Field = field;
            Dir = dir;
        }

        public string Field { get; set; }
        public string Dir { get; set; }
    }
}