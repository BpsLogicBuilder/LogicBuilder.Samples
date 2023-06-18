using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.BSL.Controllers
{
    [Produces("application/json")]
    [Route("api/MoreInfo")]
    public class MoreInfoController : Controller
    {
        private readonly IFlowManager flowManager;

        public MoreInfoController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteMoreInfoRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteMoreInfoRequest;
            this.flowManager.Start("deletemoreInfo");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveMoreInfoRequest)
        {
            this.flowManager.FlowDataCache.Request = saveMoreInfoRequest;
            this.flowManager.Start("savemoreInfo");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
