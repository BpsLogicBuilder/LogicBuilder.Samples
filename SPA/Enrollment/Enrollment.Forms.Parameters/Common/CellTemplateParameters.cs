using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class CellTemplateParameters
    {
        public CellTemplateParameters()
        {
        }

        public CellTemplateParameters(string templateName)
        {
            TemplateName = templateName;
        }

        public string TemplateName { get; set; }
    }
}
