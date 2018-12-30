using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enrollment.Data.Rules;
using Enrollment.Domain.Entities;
using Enrollment.Repositories;
using Enrollment.Web.Flow;
using Enrollment.Web.Flow.Rules;
using LogicBuilder.DataContracts;
using LogicBuilder.RulesDirector;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Enrollment.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Transfer")]
    public class TransferController : Controller
    {
        private readonly ILogger<TransferController> _logger;
        private readonly IRulesRepository _rulesRepository;
        private readonly IRulesCache _rulesCache;
        private readonly IRulesManager _rulesManager;
        private readonly IFlowManager _flowManager;
        public TransferController(ILogger<TransferController> logger, IRulesRepository rulesRepository, IRulesCache rulesCache, IRulesManager rulesManager, IFlowManager flowManager)
        {
            _logger = logger;
            _rulesRepository = rulesRepository;
            this._rulesCache = rulesCache;
            this._rulesManager = rulesManager;
            this._flowManager = flowManager;
        }

        [HttpPost("PostFileData")]
        public async Task<IActionResult> PostFileData([FromBody]ModuleData value)
        {
            _logger.LogInformation("File Posted1 Before Save " + value.ModuleName);

            try
            {
                dynamic existing = await _rulesRepository.QueryAsync<RulesModuleModel, RulesModule, object, object>
                (
                    q => q.Select(r => new { r.RulesModuleId, r.Application, r.Name })
                                .SingleOrDefault(r => r.Application == value.Application && r.Name == value.ModuleName)
                );

                RulesModuleModel model = new RulesModuleModel
                {
                    Application = value.Application,
                    Name = value.ModuleName,
                    RuleSetFile = value.RulesStream,
                    ResourceSetFile = value.ResourcesStream,
                    LastUpdated = DateTime.UtcNow,
                    RulesModuleId = existing == null ? 0 : existing.RulesModuleId,
                    EntityState = existing == null ? LogicBuilder.Domain.EntityStateType.Added : LogicBuilder.Domain.EntityStateType.Modified
                };

                if (await this._rulesRepository.SaveAsync<RulesModuleModel, RulesModule>(model))
                {
                    _logger.LogInformation("Saved " + value.ModuleName);
                    await this._rulesManager.LoadSelectedModules(new List<string> { model.Name });
                    return Created($"/api/[controller]/{model.RulesModuleId}", model);
                }
                else
                {
                    _logger.LogInformation("Not Saved " + value.ModuleName);
                    return BadRequest("Not Saved");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                _logger.LogInformation("Exception on Save " + value.ModuleName);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostVariableMetaData")]
        public async Task<IActionResult> PostVariableMetaData([FromBody]VariableMetaData value)
        {
            _logger.LogInformation("File Posted1 Before Save VariableMetaData");
            try
            {
                dynamic existing = await _rulesRepository.QueryAsync<VariableMetaDataModel, Enrollment.Data.Automatic.VariableMetaData, object, object>
                (
                    q => q.Select(r => new { r.VariableMetaDataId, r.LastUpdated })
                            .OrderByDescending(r => r.LastUpdated)
                            .FirstOrDefault()
                );

                VariableMetaDataModel model = new VariableMetaDataModel
                {
                    VariableMetaDataId = existing == null ? 0 : existing.VariableMetaDataId,
                    EntityState = existing == null ? LogicBuilder.Domain.EntityStateType.Added : LogicBuilder.Domain.EntityStateType.Modified,
                    Data = value.XmlData,
                    LastUpdated = DateTime.UtcNow
                };

                if (await this._rulesRepository.SaveAsync<VariableMetaDataModel, Enrollment.Data.Automatic.VariableMetaData>(model))
                {
                    _logger.LogInformation("Saved VariableMetaData.");
                    return Created($"/api/[controller]/{model.VariableMetaDataId}", model);
                }
                else
                {
                    _logger.LogInformation("VariableMetaData Not Saved ");
                    return BadRequest("Not Saved");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Saving VariableMetaData.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteRules")]
        public async Task<IActionResult> DeleteRules([FromBody]DeleteRulesData data)
        {
            _logger.LogInformation("File Posted1 Before Deletes " + data.Application);
            try
            {
                if (await this._rulesRepository.DeleteAsync<RulesModuleModel, RulesModule>(r => r.Application == data.Application && new HashSet<string>(data.Files).Contains(r.Name)))
                {
                    _logger.LogInformation(string.Format("Deleted. {0}", string.Join(", ", data.Files)));
                    return Ok($"/api/[controller]/");
                }
                else
                {
                    _logger.LogInformation("Files Not Deleted.");
                    return BadRequest("Files Not Deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Deleteing files.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteAllRules")]
        public async Task<IActionResult> DeleteAllRules([FromBody]DeleteAllRulesData data)
        {
            _logger.LogInformation("File Posted1 Before Deletes " + data.Application);
            try
            {
                if (await this._rulesRepository.DeleteAsync<RulesModuleModel, RulesModule>(r => r.Application == data.Application))
                {
                    _logger.LogInformation(string.Format("Deleted. {0}", data.Application));
                    return Ok($"/api/[controller]/");
                }
                else
                {
                    _logger.LogInformation("Files Not Deleted.");
                    return BadRequest("Files Not Deleted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception Deleteing files.");
                return BadRequest(ex.Message);
            }
        }
    }
}