using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class DetailGroupTemplateParameters
    {
        public DetailGroupTemplateParameters()
        {
        }

        public DetailGroupTemplateParameters
        (
            [Comments("HTML template for the group.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "groupTemplate")]
            string templateName
        )
        {
            TemplateName = templateName;
        }

        public string TemplateName { get; set; }
    }
}
