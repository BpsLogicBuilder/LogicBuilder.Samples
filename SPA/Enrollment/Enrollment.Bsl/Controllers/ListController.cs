using AutoMapper;
using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Bsl.Utils;
using Enrollment.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Enrollment.Bsl.Controllers
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
