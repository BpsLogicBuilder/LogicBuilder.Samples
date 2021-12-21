using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.BSL.Controllers
{
    [Produces("application/json")]
    [Route("api/Personal")]
    public class PersonalController : Controller
    {
        private readonly IFlowManager flowManager;

        public PersonalController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deletePersonalRequest)
        {
            this.flowManager.FlowDataCache.Request = deletePersonalRequest;
            this.flowManager.Start("deletepersonal");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest savePersonalRequest)
        {
            this.flowManager.FlowDataCache.Request = savePersonalRequest;
            this.flowManager.Start("savepersonal");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
