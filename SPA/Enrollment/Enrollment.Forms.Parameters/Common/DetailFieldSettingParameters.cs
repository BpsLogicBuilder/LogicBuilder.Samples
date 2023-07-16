using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class DetailFieldSettingParameters : DetailItemParameters
    {
        public DetailFieldSettingParameters()
        {
        }

        public DetailFieldSettingParameters
        (
            [Comments("Update modelType first. Source property name from the target object.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string field,

            [Comments("Title")]
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            string title,

            [Comments("text/numeric/boolean/date")]
            [Domain("text,numeric,boolean,date")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string type,

            [Comments("HTML template for the field.")]
            DetailFieldTemplateParameters fieldTemplate = null,

            [Comments("HTML template for the field.")]
            DetailDropDownTemplateParameters valueTextTemplate = null,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            Field = field;
            Title = title;
            Type = type;
            FieldTemplate = fieldTemplate;
            ValueTextTemplate = valueTextTemplate;
            ModelType = modelType;
        }

        public override DetailItemEnum DetailType => DetailItemEnum.Field;
        public string Field { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DetailFieldTemplateParameters FieldTemplate { get; set; }
        public DetailDropDownTemplateParameters ValueTextTemplate { get; set; }
        public string ModelType { get; set; }
    }
}
