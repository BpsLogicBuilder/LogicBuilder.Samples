using Contoso.Common.Configuration.ExpansionDescriptors;

namespace Contoso.Forms.View.Common
{
    public class GridRequestDetailsView
    {
        public string ModelType { get; set; }
        public string DataType { get; set; }
        public string DataSourceUrl { get; set; }
        public SelectExpandDefinitionDescriptor SelectExpandDefinition { get; set; }
    }
}
