using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class DetailFormSettingsParameters
    {
        public DetailFormSettingsParameters()
        {
        }

        public DetailFormSettingsParameters
        (
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            [Comments("Header field on the form")]
            string title,

            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            [Comments("Update modelType first. This field is displayed next to the title.")]
            string displayField,

            [Comments("Includes the URL to retrieve the data.")]
            FormRequestDetailsParameters requestDetails,

            [Comments("List of fields and form groups for display.")]
            List<DetailItemParameters> fieldSettings,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = "Enrollment.Domain.Entities"
        )
        {
            Title = title;
            DisplayField = displayField;
            RequestDetails = requestDetails;
            FieldSettings = fieldSettings;
        }

        public string Title { get; set; }
        public string DisplayField { get; set; }
        public FormRequestDetailsParameters RequestDetails { get; set; }
        public List<DetailItemParameters> FieldSettings { get; set; }
    }
}
