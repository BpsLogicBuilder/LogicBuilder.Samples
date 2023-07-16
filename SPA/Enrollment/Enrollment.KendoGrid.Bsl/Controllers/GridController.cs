using AutoMapper;
using Enrollment.KendoGrid.Bsl.Business.Requests;
using Enrollment.KendoGrid.Bsl.Utils;
using Enrollment.Repositories;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Enrollment.KendoGrid.Bsl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GridController : Controller
    {
        private readonly IMapper mapper;
        private readonly IEnrollmentRepository repository;

        public GridController(IMapper mapper, IEnrollmentRepository repository)
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
