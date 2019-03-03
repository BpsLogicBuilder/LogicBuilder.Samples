using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class AggregateTemplateParameters
    {
        public AggregateTemplateParameters()
        {
        }

        public AggregateTemplateParameters(string templateName, List<AggregateTemplateFieldsParameters> aggregates)
        {
            TemplateName = templateName;
            Aggregates = aggregates;
        }

        public string TemplateName { get; set; }
        public List<AggregateTemplateFieldsParameters> Aggregates { get; set; }
    }
}
