using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class FilterTemplateParameters
    {
        public FilterTemplateParameters()
        {
        }

        public FilterTemplateParameters
        (
            string templateName,

            bool isPrimitive,

            [Comments("Update modelType first. Property name for the text field. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string textField,

            [Comments("Update modelType first. Property name for the value field. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string valueField,

            DataRequestStateParameters state,

            RequestDetailsParameters requestDetails,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "CheckMySymptoms.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            TemplateName = templateName;
            IsPrimitive = isPrimitive;
            TextField = textField;
            ValueField = valueField;
            State = state;
            RequestDetails = requestDetails;
        }

        public string TemplateName { get; set; }
        public bool IsPrimitive { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }
        public DataRequestStateParameters State { get; set; }
        public RequestDetailsParameters RequestDetails { get; set; }
    }
}
