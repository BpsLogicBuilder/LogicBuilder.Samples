using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Common
{
    public class DetailDropDownTemplateParameters
    {
        public DetailDropDownTemplateParameters()
        {
        }

        public DetailDropDownTemplateParameters
        (
            [Comments("HTML template for the drop-down.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "dropDownTemplate")]
            string templateName,

            [Comments("Place holder text.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "Select One ...")]
            string placeHolderText,

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

            [Comments("Details about the drop-down data source including joins and partial field set (selects).")]
            RequestDetailsParameters requestDetails,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = "Contoso.Domain.Entities"
        )
        {
            TemplateName = templateName;
            PlaceHolderText = placeHolderText;
            TextField = textField;
            ValueField = valueField;
            TextAndValueSelector = textAndValueSelector;
            RequestDetails = requestDetails;
        }

        public string TemplateName { get; set; }
        public string PlaceHolderText { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }
        public SelectorLambdaOperatorParameters TextAndValueSelector { get; set; }
        public RequestDetailsParameters RequestDetails { get; set; }
    }
}
