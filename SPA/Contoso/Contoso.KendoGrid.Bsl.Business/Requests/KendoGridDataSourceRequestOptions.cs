namespace Contoso.KendoGrid.Bsl.Business.Requests
{
    public class KendoGridDataSourceRequestOptions
    {
        public string Aggregate { get; set; }
        public string Filter { get; set; }
        public string Group { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public int PageSize { get; set; }
    }
}
