using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.Requests
{
    public class CommandButtonRequest
    {
        public string NewSelection { get; set; }
        public bool Cancel { get; set; }
    }
}
