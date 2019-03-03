using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class EditFormSettingsView : ViewBase
    {
		public string Title { get; set; }
		public string DisplayField { get; set; }
		public RequestDetailsView RequestDetails { get; set; }
		public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
		public List<FormItemSettingView> FieldSettings { get; set; }
		public FilterGroupView FilterGroup { get; set; }
		public Dictionary<string, List<DirectiveView>> ConditionalDirectives { get; set; }
		public string ModelType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(EditFormSettingsView))
                return false;

            EditFormSettingsView other = (EditFormSettingsView)obj;

            return other.Title == this.Title
                && other.DisplayField == this.DisplayField;
        }

        public override int GetHashCode()
            => (this.Title ?? string.Empty).GetHashCode();

        public override void UpdateFields(object fields)
        {
        }
    }
}