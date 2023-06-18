using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Enrollment.Bsl.Controllers
{
    [Produces("application/json")]
    [Route("api/Person")]
    public class PersonController : Controller
    {
        private readonly IFlowManager flowManager;

        public PersonController(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] DeleteEntityRequest deletePersonRequest)
        {
            this.flowManager.FlowDataCache.Request = deletePersonRequest;
            this.flowManager.Start("deleteperson");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] SaveEntityRequest savePersonRequest)
        {
            this.flowManager.FlowDataCache.Request = savePersonRequest;
            this.flowManager.Start("saveperson");
            return Ok(this.flowManager.FlowDataCache.Response);
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "a", "b" };
        }
    }
}
