using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class CellListTemplateParameters
    {
        public CellListTemplateParameters()
        {
        }

        public CellListTemplateParameters
        (
            [Comments("HTML template name for the cell.")]
            string templateName,

            [Comments("Update modelType first. Source property name from the target object. Use a period for nested fields i.e. foo.bar.")]
            [ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
            [NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
            string displayMember,

            [ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = "CheckMySymptoms.Domain.Entities"
        )
        {
            TemplateName = templateName;
            DisplayMember = displayMember;
        }

        public string TemplateName { get; set; }
        public string DisplayMember { get; set; }
    }
}
