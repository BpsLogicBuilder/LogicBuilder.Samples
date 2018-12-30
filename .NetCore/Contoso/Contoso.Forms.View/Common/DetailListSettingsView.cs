using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class DetailListSettingsView : DetailItemView
    {
		public override DetailItemEnum DetailType { get; set; }
		public string Field { get; set; }
		public string Title { get; set; }
		public DetailListTemplateView ListTemplate { get; set; }
		public List<DetailItemView> FieldSettings { get; set; }
    }
}