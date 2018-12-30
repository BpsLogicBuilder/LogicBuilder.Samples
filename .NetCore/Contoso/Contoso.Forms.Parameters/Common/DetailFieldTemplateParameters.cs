using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class DetailFieldTemplateParameters
    {
        public DetailFieldTemplateParameters()
        {
        }

        public DetailFieldTemplateParameters
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
