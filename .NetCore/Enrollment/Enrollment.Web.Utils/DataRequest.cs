using Enrollment.Forms.View.Expansions;
using System.Collections.Generic;

namespace Enrollment.Web.Utils
{
    public class DataRequest
    {
        public DataSourceRequestOptions Options { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
        public IEnumerable<string> Includes { get; set; }
        public IDictionary<string, string> Selects { get; set; }
        public SelectExpandDefinitionView SelectExpandDefinition { get; set; }
        public bool? Distinct { get; set; }
    }
}
