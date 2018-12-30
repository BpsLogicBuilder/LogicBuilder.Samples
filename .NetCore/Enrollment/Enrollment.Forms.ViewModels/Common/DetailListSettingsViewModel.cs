using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class DetailListSettingsViewModel : DetailItemViewModel
    {
		public override DetailItemEnum DetailType { get; set; }
		public string Field { get; set; }
		public string Title { get; set; }
		public DetailListTemplateViewModel ListTemplate { get; set; }
		public List<DetailItemViewModel> FieldSettings { get; set; }
    }
}