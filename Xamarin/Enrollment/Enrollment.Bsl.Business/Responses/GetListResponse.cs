using Enrollment.Domain;
using System.Collections.Generic;

namespace Enrollment.Bsl.Business.Responses
{
    public class GetListResponse : BaseResponse
    {
        public IEnumerable<EntityModelBase> List { get; set; }
    }
}
