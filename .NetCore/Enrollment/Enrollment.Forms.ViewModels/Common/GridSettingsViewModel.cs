using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class GridSettingsViewModel
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
        public List<ColumnSettingsViewModel> Columns { get; set; }
		public int? GridId { get; set; }
		public FilterGroupViewModel ItemFilter { get; set; }
		public int? Height { get; set; }
		public CommandColumnViewModel CommandColumn { get; set; }
		public DataRequestStateViewModel State { get; set; }
		public List<AggregateDefinitionViewModel> Aggregates { get; set; }
		public RequestDetailsViewModel RequestDetails { get; set; }
		public GridSettingsViewModel DetailGridSettings { get; set; }
    }
}