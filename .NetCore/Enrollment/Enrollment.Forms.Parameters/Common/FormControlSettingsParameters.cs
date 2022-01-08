using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class FormControlSettingsParameters : FormItemSettingParameters
    {
        public FormControlSettingsParameters()
        {
        }

        public FormControlSettingsParameters
        (
            [Comments("Update modelType first. Source property name from the target object.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Comments("ID attribute for the DOM element - usually (field)_id - also used on the label's for attribute.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "(field)_id")]
            string domElementId,

            [Comments("Title")]
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            string title,

            [Comments("Place holder text.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "(Title) required")]
            string placeHolder,

            [Comments("text/numeric/boolean/date/email")]
            [Domain("text,numeric,boolean,date,email")]
            [ParameterEditorControl(ParameterControlType.SingleLineTextBox)]
            string type,

            [Comments("Defines the field's default value, validation functions (and arguments for the validator where necessary).")]
            FormValidationSettingParameters validationSetting = null,

            [Comments("HTML template applicable to input elements.")]
            TextFieldTemplateParameters textTemplate = null,

            [Comments("HTML template applicable to drop-down elements.")]
            DropDownTemplateParameters dropDownTemplate = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            Field = field;
            DomElementId = domElementId;
            Title = title;
            Placeholder = placeHolder;
            Type = type;
            ValidationSetting = validationSetting;
            TextTemplate = textTemplate;
            DropDownTemplate = dropDownTemplate;
        }

        public override AbstractControlEnum AbstractControlType { get => AbstractControlEnum.FormControl; }
        public string Field { get; set; }
        public string DomElementId { get; set; }
        public string Title { get; set; }
        public string Placeholder { get; set; }
        public string Type { get; set; }
        public FormValidationSettingParameters ValidationSetting { get; set; }
        public TextFieldTemplateParameters TextTemplate { get; set; }
        public DropDownTemplateParameters DropDownTemplate { get; set; }   
    }
}
