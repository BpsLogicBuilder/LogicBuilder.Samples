using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Utils;
using Contoso.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contoso.Bsl.Controllers
{
    [Produces("application/json")]
    [Route("api/List")]
    public class ListController : Controller
    {
        private readonly IMapper mapper;
        private readonly ISchoolRepository repository;

        public ListController(IMapper mapper, ISchoolRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpPost("GetList")]
        public async Task<BaseResponse> GetList([FromBody] GetTypedListRequest request)
        {
            return await RequestHelpers.GetList
            (
                request,
                repository,
                mapper
            );
        }
    }
}
