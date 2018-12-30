using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class DetailFieldSettingView : DetailItemView
    {
		public override DetailItemEnum DetailType { get; set; }
		public string Field { get; set; }
		public string Title { get; set; }
		public string Type { get; set; }
		public DetailFieldTemplateView FieldTemplate { get; set; }
		public DetailDropDownTemplateView ValueTextTemplate { get; set; }
    }
}