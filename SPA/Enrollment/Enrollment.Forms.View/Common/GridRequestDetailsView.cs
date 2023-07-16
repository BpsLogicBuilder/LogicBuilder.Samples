using Enrollment.Common.Configuration.ExpansionDescriptors;

namespace Enrollment.Forms.View.Common
{
    public class GridRequestDetailsView
    {
        public string ModelType { get; set; }
        public string DataType { get; set; }
        public string DataSourceUrl { get; set; }
        public SelectExpandDefinitionDescriptor SelectExpandDefinition { get; set; }
    }
}
