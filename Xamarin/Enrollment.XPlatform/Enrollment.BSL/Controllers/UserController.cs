using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.BSL.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IFlowManager flowManager;

        public UserController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteUserRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteUserRequest;
            this.flowManager.Start("deleteuser");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveUserRequest)
        {
            this.flowManager.FlowDataCache.Request = saveUserRequest;
            this.flowManager.Start("saveuser");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
