using Enrollment.Spa.Flow.Rules;
using LogicBuilder.DataContracts;
using LogicBuilder.RulesDirector;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.Spa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IRulesCache _rulesCache;
        private readonly IRulesLoader _rulesLoader;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TransferController(IRulesCache rulesCache, IRulesLoader rulesLoader, IWebHostEnvironment webHostEnvironment)
        {
            _rulesCache = rulesCache;
            _rulesLoader = rulesLoader;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("PostFileData")]
        public async Task<IActionResult> PostFileData([FromBody] ModuleData moduleData)
        {
            try
            {
                if (_webHostEnvironment.EnvironmentName != "Development")
                {
                    throw new InvalidOperationException(
                        "This shouldn't be invoked in non-development environments.");
                }

                await _rulesLoader.LoadRules
                (
                    new Domain.Entities.RulesModuleModel
                    {
                        Name = moduleData.ModuleName.ToLowerInvariant(),
                        RuleSetFile = moduleData.RulesStream,
                        ResourceSetFile = moduleData.ResourcesStream
                    },
                    _rulesCache
                );

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
