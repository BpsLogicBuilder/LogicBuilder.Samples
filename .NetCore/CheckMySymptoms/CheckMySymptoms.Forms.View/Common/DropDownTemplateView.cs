﻿using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class DropDownTemplateView
    {
		public string TemplateName { get; set; }
		public string PlaceHolderText { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
        public string ListId { get; set; }
        public DataRequestStateView State { get; set; }
		public RequestDetailsView RequestDetails { get; set; }
    }
}