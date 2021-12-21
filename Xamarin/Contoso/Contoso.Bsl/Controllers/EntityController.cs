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
