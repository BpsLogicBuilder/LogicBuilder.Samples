using AutoMapper;
using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Bsl.Utils;
using Enrollment.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Enrollment.Bsl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousTypeListController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMyRepository repository;

        public AnonymousTypeListController(IMapper mapper, IMyRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpPost("GetList")]
        public async Task<BaseResponse> GetList([FromBody] GetObjectListRequest request)
        {
            return await RequestHelpers.GetAnonymousList
            (
                request,
                repository,
                mapper
            );
        }
    }
}
