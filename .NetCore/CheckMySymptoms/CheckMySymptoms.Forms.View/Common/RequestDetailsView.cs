using System.Collections.Generic;

namespace CheckMySymptoms.Forms.View.Common
{
    public class RequestDetailsView
    {
		public string ModelType { get; set; }
		public string DataType { get; set; }
		public string DataSourceUrl { get; set; }
		public string GetUrl { get; set; }
		public string AddUrl { get; set; }
		public string UpdateUrl { get; set; }
		public string DeleteUrl { get; set; }
		public string[] Includes { get; set; }
		public Dictionary<string, string> Selects { get; set; }
		public bool? Distinct { get; set; }
    }
}