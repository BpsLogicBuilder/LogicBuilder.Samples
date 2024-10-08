﻿using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class DetailFormSettingsView
    {
        public string Title { get; set; }
        public string DisplayField { get; set; }
        public FormRequestDetailsView RequestDetails { get; set; }
        public List<DetailItemView> FieldSettings { get; set; }
    }
}