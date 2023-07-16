using Enrollment.Common.Configuration.ExpansionDescriptors;

namespace Enrollment.Forms.View.Common
{
    public class RequestDetailsView
    {
        public string ModelType { get; set; }
        public string DataType { get; set; }
        public string ModelReturnType { get; set; }
        public string DataReturnType { get; set; }
        public string DataSourceUrl { get; set; }
        public SelectExpandDefinitionDescriptor SelectExpandDefinition { get; set; }
    }
}