﻿using Enrollment.Spa.Flow;
using Enrollment.Spa.Flow.Options;
using Enrollment.Spa.Flow.Requests;
using Enrollment.Spa.Flow.Requests.TransientFlows;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Enrollment.Spa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowController : ControllerBase
    {
        private readonly IFlowManager _flowManager;
        private readonly ITransientFlowHelper _transientFlowHelper;
        private readonly InitialOptions _initialOptions;

        public FlowController(IFlowManager flowManager,
            ITransientFlowHelper transientFlowHelper,
            IOptions<InitialOptions> optionsAccessor)
        {
            this._flowManager = flowManager;
            _transientFlowHelper = transientFlowHelper;
            _initialOptions = optionsAccessor.Value;
        }

        [HttpGet]
        public IActionResult Get()
            => Ok(this._flowManager.Start(_initialOptions.InitialModule, _initialOptions.TargetModule));

        [HttpPost("Start")]
        public IActionResult Start()
        {
            return Ok(_flowManager.Start(_initialOptions.InitialModule, _initialOptions.TargetModule));
        }

        [HttpPost("NavStart")]
        public IActionResult NavStart([FromBody] NavBarRequest navBarRequest)
        {
            return Ok(this._flowManager.NavStart(navBarRequest));
        }

        [HttpPost("GetSelector")]
        public IActionResult GetSelector([FromBody] SelectorFlowRequest selectorFlowRequest)
        {
            return Ok(_transientFlowHelper.RunSelectorFlow(selectorFlowRequest));
        }

        [HttpPost("Next")]
        public IActionResult Next([FromBody] RequestBase response)
        {
            return Ok(this._flowManager.Next(response));
        }
    }
}
