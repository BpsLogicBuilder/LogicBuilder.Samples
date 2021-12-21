using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Bsl.Controllers
{
    [Produces("application/json")]
    [Route("api/Department")]
    public class DepartmentController : Controller
    {
        private readonly IFlowManager flowManager;

        public DepartmentController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteDepartmentRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteDepartmentRequest;
            this.flowManager.Start("deletedepartment");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveDepartmentRequest)
        {
            this.flowManager.FlowDataCache.Request = saveDepartmentRequest;
            this.flowManager.Start("savedepartment");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
