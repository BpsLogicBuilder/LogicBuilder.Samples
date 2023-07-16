using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class DetailListTemplateParameters
    {
        public DetailListTemplateParameters()
        {
        }

        public DetailListTemplateParameters
        (
            [Comments("HTML template for the list.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "listTemplate")]
            string templateName
        )
        {
            TemplateName = templateName;
        }

        public string TemplateName { get; set; }
    }
}
