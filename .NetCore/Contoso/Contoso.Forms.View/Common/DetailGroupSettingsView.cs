using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class DetailGroupSettingsView : DetailItemView
    {
		public override DetailItemEnum DetailType { get; set; }
		public string Field { get; set; }
		public string Title { get; set; }
		public DetailGroupTemplateView GroupTemplate { get; set; }
		public List<DetailItemView> FieldSettings { get; set; }
    }
}