using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.BSL.Controllers
{
    [Produces("application/json")]
    [Route("api/Academic")]
    public class AcademicController : Controller
    {
        private readonly IFlowManager flowManager;

        public AcademicController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteAcademicRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteAcademicRequest;
            this.flowManager.Start("deleteacademic");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveAcademicRequest)
        {
            this.flowManager.FlowDataCache.Request = saveAcademicRequest;
            this.flowManager.Start("saveacademic");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
