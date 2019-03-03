using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class MessageTemplateView : ViewBase
    {
		public string Caption { get; set; }
		public string Message { get; set; }
		public string TemplateName { get; set; }
		public string Icon { get; set; }

        public string Selection { get; set; } = "";

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(MessageTemplateView))
                return false;

            MessageTemplateView other = (MessageTemplateView)obj;

            return other.Caption == this.Caption
                && other.Message == this.Message
                && other.TemplateName == this.TemplateName;
        }

        public override int GetHashCode()
            => (this.Message ?? string.Empty).GetHashCode();

        public override void UpdateFields(object fields)
        {
            Selection = fields is string ? (string)fields : string.Empty;
        }
    }
}