using Contoso.Domain;

namespace Contoso.Bsl.Business.Requests
{
    public class SaveEntityRequest : BaseRequest
    {
        public EntityModelBase Entity { get; set; }
    }
}
