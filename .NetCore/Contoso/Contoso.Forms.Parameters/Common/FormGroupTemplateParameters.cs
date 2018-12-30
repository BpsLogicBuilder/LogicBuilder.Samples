using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class FormGroupTemplateParameters
    {
        public FormGroupTemplateParameters()
        {
        }

        public FormGroupTemplateParameters
        (
            [Comments("HTML template for the form group.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "formGroupTemplate")]
            string templateName
        )
        {
            TemplateName = templateName;
        }

        public string TemplateName { get; set; }
    }
}
