using Contoso.Domain;

namespace Contoso.Web.Utils
{
    public class PostModelRequest
    {
        public DataSourceRequestOptions Options { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
        public BaseModelClass Model { get; set; }
    }
}
