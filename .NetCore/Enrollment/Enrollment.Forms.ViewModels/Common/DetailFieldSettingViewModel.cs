using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class DetailFieldSettingViewModel : DetailItemViewModel
    {
		public override DetailItemEnum DetailType { get; set; }
		public string Field { get; set; }
		public string Title { get; set; }
		public string Type { get; set; }
		public DetailFieldTemplateViewModel FieldTemplate { get; set; }
		public DetailDropDownTemplateViewModel ValueTextTemplate { get; set; }
    }
}