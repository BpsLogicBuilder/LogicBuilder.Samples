using CheckMySymptoms.Forms.Parameters.Common;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.Parameters.Input
{
    public class InputDataParameters : BaseDataParameters
    {
        public InputDataParameters
        (
            [Comments("Form label.")]
            string text,

            [Comments("CSS class.")]
            string classAttribute,

            [Comments("Tool tip text.")]
            string toolTipText,

            [Comments("Place holder text.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "(Title) is required.")]
            string placeholder,

            [Comments("text,numeric,boolean,date")]
            [Domain("text,numeric,boolean,date")]
            [NameValue(AttributeNames.DEFAULTVALUE, "text")]
            string htmlType,

            [Comments("HTML template applicable to input elements.")]
            TextFieldTemplateParameters textTemplate = null,

            [Comments("HTML template applicable to drop-down elements.")]
            DropDownTemplateParameters dropDownTemplate = null,

            [Comments("HTML template applicable to multi-select elements.")]
            MultiSelectTemplateParameters multiSelectTemplate = null,

            [Comments("Directives for conditionally performing UI actions e.g. hide, disable etc.")]
            List<DirectiveParameters> directives = null,

            [Comments("Defines the field's default value, validation functions (and arguments for the validator where necessary).")]
            FormValidationSettingParameters validationSetting = null,

            [Comments("False if the variable can be modified on the client otherwise true.")]
            [Domain("true,false")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            bool? readOnly = false
        )
            : base(text, classAttribute, toolTipText, placeholder, htmlType, textTemplate, dropDownTemplate, multiSelectTemplate, directives, validationSetting, readOnly)
        {
        }

        protected InputDataParameters() { }
    }
}
