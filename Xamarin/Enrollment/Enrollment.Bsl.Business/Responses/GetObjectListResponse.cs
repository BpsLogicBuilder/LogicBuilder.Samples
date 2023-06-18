using System.Collections.Generic;

namespace Enrollment.Bsl.Business.Responses
{
    public class GetObjectListResponse : BaseResponse
    {
        public IEnumerable<object> List { get; set; }
    }
}
