using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class ColumnSettingsView
    {
        public string Field { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool? Groupable { get; set; }
        public int? Width { get; set; }
        public string Format { get; set; }
        public string Filter { get; set; }
        public CellTemplateView CellTemplate { get; set; }
        public CellListTemplateView CellListTemplate { get; set; }
        public FilterTemplateView FilterRowTemplate { get; set; }
        public FilterTemplateView FilterMenuTemplate { get; set; }
        public AggregateTemplateView GroupHeaderTemplate { get; set; }
        public AggregateTemplateView GroupFooterTemplate { get; set; }
        public AggregateTemplateView GridFooterTemplate { get; set; }
    }
}