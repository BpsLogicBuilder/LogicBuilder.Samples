using System.Collections.Generic;

namespace Contoso.Forms.View.Common
{
    public class GridSettingsView
    {
		public string Title { get; set; }
		public bool Sortable { get; set; }
		public bool Pageable { get; set; }
		public string Scrollable { get; set; }
		public bool Groupable { get; set; }
		public bool IsFilterable { get; set; }
		public string FilterableType { get; set; }
		public object Filterable => string.IsNullOrEmpty(FilterableType)
                                        ? (object)IsFilterable
                                        : FilterableType;
        public List<ColumnSettingsView> Columns { get; set; }
		public int? GridId { get; set; }
		public FilterGroupView ItemFilter { get; set; }
		public int? Height { get; set; }
		public CommandColumnView CommandColumn { get; set; }
		public DataRequestStateView State { get; set; }
		public List<AggregateDefinitionView> Aggregates { get; set; }
		public RequestDetailsView RequestDetails { get; set; }
		public GridSettingsView DetailGridSettings { get; set; }
    }
}