using System.Collections.Generic;

namespace Contoso.Bsl.Business.Responses
{
    public class GetObjectListResponse : BaseResponse
    {
        public IEnumerable<object> List { get; set; }
    }
}
