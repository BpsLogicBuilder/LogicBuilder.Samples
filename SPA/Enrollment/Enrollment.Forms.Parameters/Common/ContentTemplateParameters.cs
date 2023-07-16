using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class ContentTemplateParameters
    {
        public ContentTemplateParameters()
        {

        }

        public ContentTemplateParameters
        (
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            [Comments("Header text for the page")]
            string title,

            [Comments("HTML template for the page markup.")]
            [NameValue(AttributeNames.DEFAULTVALUE, "(customTemplate)")]
            string templateName
        )
        {
            Title = title;
            TemplateName = templateName;
        }

        public string Title { get; set; }
        public string TemplateName { get; set; }
    }
}
