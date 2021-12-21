using Contoso.Domain.Entities;
using LogicBuilder.Workflow.Activities.Rules;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;

namespace Contoso.XPlatform.Flow.Rules
{
    public class RulesLoader : IRulesLoader
    {
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
    }
}
