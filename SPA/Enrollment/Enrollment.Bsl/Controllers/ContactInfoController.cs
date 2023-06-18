using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.BSL.Controllers
{
    [Produces("application/json")]
    [Route("api/ContactInfo")]
    public class ContactInfoController : Controller
    {
        private readonly IFlowManager flowManager;

        public ContactInfoController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deleteContactInfoRequest)
        {
            this.flowManager.FlowDataCache.Request = deleteContactInfoRequest;
            this.flowManager.Start("deletecontactInfo");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest saveContactInfoRequest)
        {
            this.flowManager.FlowDataCache.Request = saveContactInfoRequest;
            this.flowManager.Start("savecontactInfo");
            return Ok(this.flowManager.FlowDataCache.Response);
        }
    }
}
