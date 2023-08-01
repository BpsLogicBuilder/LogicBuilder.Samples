using Enrollment.Domain.Entities;
using LogicBuilder.RulesDirector;
using LogicBuilder.Workflow.Activities.Rules;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace Enrollment.Bsl.Flow.Rules
{
    public class RulesLoader : IRulesLoader
    {
        public Task LoadRules(RulesModuleModel module, IRulesCache cache)
        {
            return Task.Run
            (
                () =>
                {
                    string moduleName = module.Name.ToLowerInvariant();
                    RuleSet ruleSet = module.DeserializeRuleSetFile() ?? throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.invalidRulesetFormat, moduleName));

                    if (cache.RuleEngines.ContainsKey(moduleName))
                        cache.RuleEngines[moduleName] = new RuleEngine(ruleSet, RulesSerializer.GetValidation(ruleSet));
                    else
                        cache.RuleEngines.Add(moduleName, new RuleEngine(ruleSet, RulesSerializer.GetValidation(ruleSet)));

                    using IResourceReader reader = new ResourceReader(new MemoryStream(module.ResourceSetFile));
                    reader.OfType<DictionaryEntry>()
                        .ToList()
                        .ForEach(entry =>
                        {
                            string resourceKey = (string)entry.Key;
                            if (cache.ResourceStrings.ContainsKey(resourceKey))
                                cache.ResourceStrings[resourceKey] = (string)(entry.Value ?? "");
                            else
                                cache.ResourceStrings.Add(resourceKey, (string)(entry.Value ?? ""));
                        });
                }
            );
        }

        public Task LoadRulesOnStartUp(RulesModuleModel module, IRulesCache cache)
        {
            return Task.Run
            (
                () =>
                {
                    string moduleName = module.Name.ToLowerInvariant();
                    RuleSet ruleSet = module.DeserializeRuleSetFile() ?? throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.invalidRulesetFormat, moduleName));
                    cache.RuleEngines.Add(moduleName, new RuleEngine(ruleSet, RulesSerializer.GetValidation(ruleSet)));

                    using IResourceReader reader = new ResourceReader(new MemoryStream(module.ResourceSetFile));
                    reader.OfType<DictionaryEntry>()
                        .ToList()
                        .ForEach(entry => cache.ResourceStrings.Add((string)entry.Key, (string)entry.Value));
                }
            );
        }
    }
}
