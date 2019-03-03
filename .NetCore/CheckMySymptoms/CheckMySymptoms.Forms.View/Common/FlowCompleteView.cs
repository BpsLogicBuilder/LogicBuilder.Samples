using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.View.Common
{
    public class FlowCompleteView : ViewBase
    {
        public string Caption { get; set; }
        public string Message { get; set; }
        public string TemplateName { get; set; }
        public string Icon { get; set; }

        public List<string> Symptoms { get; set; }
        public List<string> Diagnoses { get; set; }
        public List<string> Treatments { get; set; }

        public override void UpdateFields(object fields)
        {
        }
    }
}
