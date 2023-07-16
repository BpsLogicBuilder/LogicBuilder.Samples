using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.BSL.Controllers
{
    [Produces("application/json")]
    [Route("api/Certification")]
    public class CertificationController : Controller
    {
        private readonly IFlowManager flowManager;

        public CertificationController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteCertificationRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteCertificationRequest;
            this.flowManager.Start("deletecertification");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveCertificationRequest)
        {
            this.flowManager.FlowDataCache.Request = saveCertificationRequest;
            this.flowManager.Start("savecertification");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
