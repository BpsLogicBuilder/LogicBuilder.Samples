using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class TextFieldTemplateParameters
    {
        public TextFieldTemplateParameters()
        {
        }

        public TextFieldTemplateParameters
        (
            [Comments("HTML template for the field.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "textTemplate")]
            string templateName
        )
        {
            TemplateName = templateName;
        }

        public string TemplateName { get; set; }
    }
}
