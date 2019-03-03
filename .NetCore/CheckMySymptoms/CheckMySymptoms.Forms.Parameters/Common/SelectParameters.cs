using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class SelectParameters
    {
        public SelectParameters()
        {
        }

        public SelectParameters
        (
            [Comments("Property name for field in the anonymous type.")]
            string fieldName,

            [Comments("Update modelType first. Source property name from the target object. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string sourceMember,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "CheckMySymptoms.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            FieldName = fieldName;
            SourceMember = sourceMember;
        }

        public string FieldName { get; set; }
        public string SourceMember { get; set; }
    }
}
