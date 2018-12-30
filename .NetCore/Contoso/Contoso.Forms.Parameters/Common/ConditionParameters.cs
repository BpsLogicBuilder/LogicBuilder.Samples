using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Common
{
    public class ConditionParameters
    {
        public ConditionParameters()
        {
        }

        public ConditionParameters
        (
            [Comments("The condition operator (comparison).")]
            [Domain("eq, neq, lt, lte, gt, gte, contains, doesnotcontain, startswith, endswith, isnotempty, isempty, isnotnull, isnull")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string @operator,

            [Comments("Left operand.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string leftVariable,

            [Comments("Right operand.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string rightVariable = null,

            [Comments("Alternative to right operand.")]
            object value = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null

        )
        {
            Operator = @operator;
            LeftVariable = leftVariable;
            RightVariable = rightVariable;
            Value = value;
        }

        public string Operator { get; set; }
        public string LeftVariable { get; set; }
        public string RightVariable { get; set; }
        public object Value { get; set; }
    }
}