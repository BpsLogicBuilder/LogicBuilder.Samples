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
    [Route("api/Entity")]
    public class EntityController : Controller
    {
        private readonly IMapper mapper;
        private readonly ISchoolRepository repository;

        public EntityController(IMapper mapper, ISchoolRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpPost("GetEntity")]
        public async Task<BaseResponse> GetEntity([FromBody] GetEntityRequest request)
            => await RequestHelpers.GetEntity
            (
                request,
                repository,
                mapper
            );
    }
}
