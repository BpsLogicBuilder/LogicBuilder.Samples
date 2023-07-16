using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Enrollment.Forms.Parameters.Common
{
    public class FormGroupBoxSettingsParameters : FormItemSettingParameters
    {
        public FormGroupBoxSettingsParameters()
        {
        }

        public FormGroupBoxSettingsParameters
        (
            [Comments("HTML template for the group box.")]
            FormGroupTemplateParameters formGroupTemplate,

            [Comments("Configuration for each field in the group box.")]
            List<FormItemSettingParameters> fieldSettings,

            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            [Comments("Title for the group box.")]
            string title,

            bool showTitle = true
        )
        {
            FormGroupTemplate = formGroupTemplate;
            FieldSettings = fieldSettings;
            Title = title;
            ShowTitle = showTitle;
        }

        public override AbstractControlEnum AbstractControlType => AbstractControlEnum.GroupBox;
        public FormGroupTemplateParameters FormGroupTemplate { get; set; }
        public List<FormItemSettingParameters> FieldSettings { get; set; }
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
    }
}
