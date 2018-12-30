using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class RequestDetailsViewModel
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