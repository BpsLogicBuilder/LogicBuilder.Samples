﻿using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class DirectiveDescriptionView
    {
        public string ClassName { get; set; }
        public string FunctionName { get; set; }
        public Dictionary<string, object> Arguments { get; set; }
    }
}