using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class ColumnSettingsViewModel
    {
		public string Field { get; set; }
		public string Title { get; set; }
		public string Type { get; set; }
		public bool? Groupable { get; set; }
		public int? Width { get; set; }
		public string Format { get; set; }
		public string Filter { get; set; }
		public CellTemplateViewModel CellTemplate { get; set; }
		public CellListTemplateViewModel CellListTemplate { get; set; }
		public FilterTemplateViewModel FilterRowTemplate { get; set; }
		public FilterTemplateViewModel FilterMenuTemplate { get; set; }
		public AggregateTemplateViewModel GroupHeaderTemplate { get; set; }
		public AggregateTemplateViewModel GroupFooterTemplate { get; set; }
		public AggregateTemplateViewModel GridFooterTemplate { get; set; }
    }
}