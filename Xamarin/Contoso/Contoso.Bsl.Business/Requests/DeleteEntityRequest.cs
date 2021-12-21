using Contoso.Domain;

namespace Contoso.Bsl.Business.Requests
{
    public class DeleteEntityRequest : BaseRequest
    {
        public EntityModelBase Entity { get; set; }
    }
}
