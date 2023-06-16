using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Common
{
    public class FilterDefinitionParameters
    {
        public FilterDefinitionParameters()
        {
        }

        public FilterDefinitionParameters
        (
            [Comments("Update modelType first. Property name from the target object. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Comments("The filter operator (comparison).")]
            [Domain("eq, neq, lt, lte, gt, gte, contains, doesnotcontain, startswith, endswith, isnotempty, isempty, isnotnull, isnull")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string oper,

            object value = null,

            [Comments("Determines if the string comparison is case-insensitive.")]
            bool? ignoreCase = null,

            [Comments("A field name on a target object from which the value field will be retrieved at run time.")]
            string valueSourceMember = "",

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            Field = field;
            Operator = oper;
            Value = value;
            IgnoreCase = ignoreCase;
            ValueSourceMember = valueSourceMember;
        }

        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        public bool? IgnoreCase { get; set; }
        public string ValueSourceMember { get; set; }
    }
}