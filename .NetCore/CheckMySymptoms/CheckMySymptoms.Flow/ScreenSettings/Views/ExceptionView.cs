using CheckMySymptoms.Forms.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Flow.ScreenSettings.Views
{
    public class ExceptionView : ViewBase
    {
        public string Caption { get; set; }
        public string Message { get; set; }

        public override void UpdateFields(object fields)
        {
        }
    }
}
