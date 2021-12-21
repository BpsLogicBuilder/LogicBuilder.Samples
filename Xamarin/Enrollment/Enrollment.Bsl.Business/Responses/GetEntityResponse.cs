using Enrollment.Domain;

namespace Enrollment.Bsl.Business.Responses
{
    public class GetEntityResponse : BaseResponse
    {
        public EntityModelBase Entity { get; set; }
    }
}
