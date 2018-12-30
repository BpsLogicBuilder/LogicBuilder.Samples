using Enrollment.Forms.Parameters.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Input
{
    public abstract class BaseDataParameters
    {
        protected BaseDataParameters
        (
            string text, 
            string classAttribute, 
            string toolTipText,
            string placeholder,
            string htmlType,
            TextFieldTemplateParameters textTemplate,
            DropDownTemplateParameters dropDownTemplate,
            MultiSelectTemplateParameters multiSelectTemplate,
            List<DirectiveParameters> directives, 
            FormValidationSettingParameters validationSetting,
            bool? readOnly
        )
        {
            Text = text;
            ClassAttribute = classAttribute;
            ToolTipText = toolTipText;
            Placeholder = placeholder;
            HtmlType = htmlType;
            TextTemplate = textTemplate;
            DropDownTemplate = dropDownTemplate;
            MultiSelectTemplate = multiSelectTemplate;
            Directives = directives;
            ValidationSetting = validationSetting;
            ReadOnly = readOnly;
        }

        protected BaseDataParameters() { }

        public string Text { get; set; }
        public string ClassAttribute { get; set; }
        public string ToolTipText { get; set; }
        public string Placeholder { get; set; }
        public string HtmlType { get; set; }
        public bool? ReadOnly { get; set; }
        public TextFieldTemplateParameters TextTemplate { get; set; }
        public DropDownTemplateParameters DropDownTemplate { get; set; }
        public MultiSelectTemplateParameters MultiSelectTemplate { get; set; }
        public List<DirectiveParameters> Directives { get; set; }
        public FormValidationSettingParameters ValidationSetting { get; set; }
    }
}
