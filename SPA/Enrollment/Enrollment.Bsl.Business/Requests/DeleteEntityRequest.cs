using Enrollment.Domain;

namespace Enrollment.Bsl.Business.Requests
{
    public class DeleteEntityRequest : BaseRequest
    {
        public EntityModelBase Entity { get; set; }
    }
}
