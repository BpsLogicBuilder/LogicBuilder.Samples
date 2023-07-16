using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.BSL.Controllers
{
    [Produces("application/json")]
    [Route("api/Residency")]
    public class ResidencyController : Controller
    {
        private readonly IFlowManager flowManager;

        public ResidencyController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteResidencyRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteResidencyRequest;
            this.flowManager.Start("deleteresidency");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveResidencyRequest)
        {
            this.flowManager.FlowDataCache.Request = saveResidencyRequest;
            this.flowManager.Start("saveresidency");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
