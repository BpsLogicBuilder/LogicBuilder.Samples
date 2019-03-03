using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class DetailListSettingsParameters : DetailItemParameters
    {
        public DetailListSettingsParameters()
        {
        }

        public DetailListSettingsParameters
        (
            [Comments("Update modelType first. Source property name from the target object.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Comments("Title")]
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            string title,

            [Comments("HTML template for the list.")]
            DetailListTemplateParameters listTemplate,

            [Comments("List of fields and form groups in the list.")]
            List<DetailItemParameters> fieldSettings,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "CheckMySymptoms.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            Field = field;
            Title = title;
            ListTemplate = listTemplate;
            FieldSettings = fieldSettings;
        }

        public override DetailItemEnum DetailType => DetailItemEnum.List;
        public string Field { get; set; }
        public string Title { get; set; }
        public DetailListTemplateParameters ListTemplate { get; set; }
        public List<DetailItemParameters> FieldSettings { get; set; }
    }
}
