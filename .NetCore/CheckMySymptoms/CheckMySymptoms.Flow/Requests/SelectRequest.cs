using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Flow.Requests
{
    public class SelectRequest : RequestBase
    {
        public MessageTemplateView MessageTemplateView { get; set; }
        public bool AddToSymptoms { get; set; }
        public override ViewType ViewType { get; set; }

        public override ViewBase View => MessageTemplateView;
    }
}
