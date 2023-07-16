using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class VariableDirectivesParameters
    {
        public VariableDirectivesParameters()
        {

        }

        public VariableDirectivesParameters
        (
            [Comments("Update modelType first. Source property name from the target object.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            [NameValue(AttributeNames.USEFOREQUALITY, "true")]
            [NameValue(AttributeNames.USEFORHASHCODE, "true")]
            string field,

            [Comments("List of conditional directives for this field.")]
            List<DirectiveParameters> conditionalDirectives,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            [NameValue(AttributeNames.USEFOREQUALITY, "false")]
            [NameValue(AttributeNames.USEFORHASHCODE, "false")]
            string modelType = null
        )
        {
            Field = field;
            ConditionalDirectives = conditionalDirectives;
        }

        public string Field { get; set; }
        public List<DirectiveParameters> ConditionalDirectives { get; set; }
    }
}
