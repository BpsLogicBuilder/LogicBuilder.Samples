using Enrollment.Domain;

namespace Enrollment.Bsl.Business.Requests
{
    public class SaveEntityRequest : BaseRequest
    {
        public EntityModelBase Entity { get; set; }
    }
}
