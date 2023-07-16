using AutoMapper;
using Contoso.KendoGrid.Bsl.Business.Requests;
using Contoso.KendoGrid.Bsl.Utils;
using Contoso.Repositories;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contoso.KendoGrid.Bsl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GridController : Controller
    {
        private readonly IMapper mapper;
        private readonly ISchoolRepository repository;

        public GridController(IMapper mapper, ISchoolRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }


        [HttpPost("GetData")]
        public Task<DataSourceResult> GetData([FromBody] KendoGridDataRequest request) 
            => RequestHelpers.GetData
            (
                request,
                repository,
                mapper
            );
    }
}
