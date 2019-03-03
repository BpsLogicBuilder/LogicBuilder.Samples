using CheckMySymptoms.Domain;
using LogicBuilder.Workflow.Activities.Rules;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CheckMySymptoms.Flow.Rules
{
    public class RulesLoader : IRulesLoader
    {
        public RulesLoader()
        {
            //this._logger = logger;
        }

        #region Fields
        //private readonly ILogger<RulesLoader> _logger;
        #endregion Fields

        #region Methods
        public async Task<RulesCache> LoadRulesOnStartUp(ICollection<RulesModuleModel> modules)
        {
            try
            {
                return await Task.Run
                (
                    () => modules.Aggregate(new RulesCache(new Dictionary<string, RuleEngine>(), new Dictionary<string, string>()), (cache, module) =>
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
                    })
                );
            }
            catch (Exception ex)
            {
                //this._logger.LogError(ex, ex.Message);
                return new RulesCache(new Dictionary<string, RuleEngine>(), new Dictionary<string, string>());
            }
        }


        public async Task LoadRulesOnStartUp(RulesModuleModel module, RulesCache cache)
        {
            await Task.Run
            (
                () =>
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
                }
            );
        }

        public void LoadRules(RulesModuleModel module, RulesCache cache)
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
        }
        #endregion Methods
    }
}
