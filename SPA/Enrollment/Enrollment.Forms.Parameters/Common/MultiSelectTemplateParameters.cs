using Enrollment.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class MultiSelectTemplateParameters
    {
        public MultiSelectTemplateParameters()
        {
        }

        public MultiSelectTemplateParameters
        (
            [Comments("HTML template for the drop-down.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "multiSelectTemplate")]
            string templateName,

            [Comments("Place holder text.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "Select One ...")]
            string placeHolderText,

            [Comments("Update modelType first. Property name for the text field. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            [NameValue(AttributeNames.DEFAULTVALUE, "text")]
            string textField,

            [Comments("Update modelType first. Property name for the value field. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            [NameValue(AttributeNames.DEFAULTVALUE, "value")]
            string valueField,

            [Comments("Single object used for defining the selector lambda expression e.g. q => q.OrderBy(s => s.FullName).Select(a => new InstructorModel() {ID = a.ID, FullName = a.FullName})")]
            SelectorLambdaOperatorParameters textAndValueSelector,

            [Comments("Details about the drop-down data source including joins and partial field set (selects).")]
            RequestDetailsParameters requestDetails,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = "Enrollment.Domain.Entities"
        )
        {
            TemplateName = templateName;
            PlaceHolderText = placeHolderText;
            TextField = textField;
            ValueField = valueField;
            TextAndValueSelector = textAndValueSelector;
            RequestDetails = requestDetails;
            ModelType = modelType;
        }

        public string TemplateName { get; set; }
        public string PlaceHolderText { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }
        public SelectorLambdaOperatorParameters TextAndValueSelector { get; set; }
        public RequestDetailsParameters RequestDetails { get; set; }
        public string ModelType { get; set; }
    }
}
