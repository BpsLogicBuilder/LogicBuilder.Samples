using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Bsl.Controllers
{
    [Produces("application/json")]
    [Route("api/Instructor")]
    public class InstructorController : Controller
    {
        private readonly IFlowManager flowManager;

        public InstructorController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteInstructorRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteInstructorRequest;
            this.flowManager.Start("deleteinstructor");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveInstructorRequest)
        {
            this.flowManager.FlowDataCache.Request = saveInstructorRequest;
            this.flowManager.Start("saveinstructor");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
