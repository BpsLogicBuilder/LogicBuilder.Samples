using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Contoso.Bsl.Controllers
{
    [Produces("application/json")]
    [Route("api/Course")]
    public class CourseController : Controller
    {
        private readonly IFlowManager flowManager;

        public CourseController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteCourseRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteCourseRequest;
            this.flowManager.Start("deletecourse");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveCourseRequest)
        {
            this.flowManager.FlowDataCache.Request = saveCourseRequest;
            this.flowManager.Start("savecourse");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
