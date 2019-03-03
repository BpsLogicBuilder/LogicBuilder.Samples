using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class HtmlPageSettingsView : ViewBase
    {
		public ContentTemplateView ContentTemplate { get; set; }
		public MessageTemplateView MessageTemplate { get; set; }
        public string Icon { get; set; }

        public override void UpdateFields(object fields)
        {
        }
    }
}