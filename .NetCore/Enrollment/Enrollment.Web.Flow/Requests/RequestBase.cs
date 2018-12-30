using Enrollment.Web.Flow.Requests.Json;
using Enrollment.Web.Flow.ScreenSettings;
using Enrollment.Web.Flow.ScreenSettings.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.Requests
{
    [JsonConverter(typeof(RequestConverter))]
    abstract public class RequestBase
    {
        public int UserId { get; set; }
        public CommandButtonRequest CommandButtonRequest { get; set; }
        public FlowState FlowState { get; set; }
        abstract public ViewType ViewType { get; set; }
    }
}
