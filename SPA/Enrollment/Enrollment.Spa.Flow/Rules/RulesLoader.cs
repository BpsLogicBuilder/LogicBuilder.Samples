using Enrollment.Domain.Entities;
using LogicBuilder.Workflow.Activities.Rules;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace Enrollment.Spa.Flow.Rules
{
    public class RulesLoader : IRulesLoader
    {
        public async Task LoadRulesOnStartUp(RulesModuleModel module, RulesCache cache)
        {
            await Task.Run
            (
                () =>
                {
                    string moduleName = module.Name.ToLowerInvariant();
                    RuleSet ruleSet = module.DeserializeRuleSetFile() ?? throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.invalidRulesetFormat, moduleName));

                    cache.RuleEngines.Add(moduleName, new RuleEngine(ruleSet, RulesSerializer.GetValidation(ruleSet)));

                    using IResourceReader reader = new ResourceReader(new MemoryStream(module.ResourceSetFile));
                    reader.OfType<DictionaryEntry>()
                        .ToList()
                        .ForEach(entry => cache.ResourceStrings.Add((string)entry.Key, (string)(entry.Value ?? "")));
                }
            );
        }
    }
}
