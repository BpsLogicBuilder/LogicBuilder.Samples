using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.BSL.Controllers
{
    [Produces("application/json")]
    [Route("api/Admissions")]
    public class AdmissionsController : Controller
    {
        private readonly IFlowManager flowManager;

        public AdmissionsController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteAdmissionsRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteAdmissionsRequest;
            this.flowManager.Start("deleteadmissions");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveAdmissionsRequest)
        {
            this.flowManager.FlowDataCache.Request = saveAdmissionsRequest;
            this.flowManager.Start("saveadmissions");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
