using Contoso.Domain;
using System.Collections.Generic;

namespace Contoso.Bsl.Business.Responses
{
    public class GetListResponse : BaseResponse
    {
        public IEnumerable<EntityModelBase> List { get; set; }
    }
}
