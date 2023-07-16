using Enrollment.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
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

            [Comments("Single object used for defining the selector lambda expression e.g. q => q.OrderBy(s => s.FullName).Select(a => new InstructorModel() {ID = a.ID, FullName = a.FullName})")]
            SelectorLambdaOperatorParameters textAndValueSelector,

            RequestDetailsParameters requestDetails,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            TemplateName = templateName;
            IsPrimitive = isPrimitive;
            TextField = textField;
            ValueField = valueField;
            TextAndValueSelector = textAndValueSelector;
            RequestDetails = requestDetails;
        }

        public string TemplateName { get; set; }
        public bool IsPrimitive { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }
        public SelectorLambdaOperatorParameters TextAndValueSelector { get; set; }
        public RequestDetailsParameters RequestDetails { get; set; }
    }
}
