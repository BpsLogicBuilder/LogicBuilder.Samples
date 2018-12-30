using Contoso.Data.Rules;
using Contoso.Domain;
using Contoso.Domain.Entities;
using Contoso.Web.Flow.Options;
using Contoso.Repositories;
using Contoso.Utils;
using LogicBuilder.Workflow.Activities.Rules;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Contoso.Web.Flow.Rules
{
    public class RulesLoader : IRulesLoader
    {
        public RulesLoader(IRulesRepository repository, IOptions<ApplicationOptions> optionsAccessor, ILogger<RulesLoader> logger)
        {
            this._repository = repository;
            this._applicationOptions = optionsAccessor.Value;
            this._logger = logger;
        }

        #region Fields
        private IRulesRepository _repository;
        private ApplicationOptions _applicationOptions;
        private ILogger<RulesLoader> _logger;
        #endregion Fields

        #region Methods
        public async Task<RulesCache> LoadRulesOnStartUp()
        {

            try
            {
                ICollection<RulesModuleModel> modules = (await this._repository.GetItemsAsync<RulesModuleModel, RulesModule>(t => t.Application == this._applicationOptions.ApplicationName));

                return modules.Aggregate(new RulesCache(new Dictionary<string, RuleEngine>(), new Dictionary<string, string>()), (cache, module) =>
                {
                    string moduleName = module.Name.ToLowerInvariant();
                    RuleSet ruleSet = module.DeserializeRuleSetFile();
                    if (ruleSet == null)
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.invalidRulesetFormat, moduleName));

                    cache.RuleEngines.Add(moduleName, new RuleEngine(ruleSet, RulesSerializer.GetValidation(ruleSet)));

                    using (IResourceReader reader = new ResourceReader(new MemoryStream(module.ResourceSetFile)))
                    {
                        reader.OfType<DictionaryEntry>()
                            .ToList()
                            .ForEach(entry => cache.ResourceStrings.Add((string)entry.Key, (string)entry.Value));
                    }

                    return cache;
                });
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return new RulesCache(new Dictionary<string, RuleEngine>(), new Dictionary<string, string>());
            }
        }
        #endregion Methods
    }
}
