using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class AggregateTemplateFieldsParameters
    {
        public AggregateTemplateFieldsParameters(string label,
            [Domain("average,count,max,min,sum")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string aggregateFunction)
        {
            Label = label;
            Function = aggregateFunction;
        }

        public string Label { get; set; }
        public string Function { get; set; }
    }
}
