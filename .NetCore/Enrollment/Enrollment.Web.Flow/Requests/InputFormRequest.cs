using Enrollment.Forms.View.Input;
using Enrollment.Web.Flow.ScreenSettings.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.Requests
{
    public class InputFormRequest : RequestBase
    {
        public InputFormView Form { get; set; }
        public override ViewType ViewType { get; set; }
    }
}
