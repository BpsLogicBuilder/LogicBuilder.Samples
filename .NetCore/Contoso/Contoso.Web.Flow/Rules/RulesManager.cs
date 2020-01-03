using Contoso.Data.Rules;
using Contoso.Domain;
using Contoso.Domain.Entities;
using Contoso.Web.Flow.Options;
using Contoso.Repositories;
using Contoso.Utils;
using LogicBuilder.RulesDirector;
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

namespace Contoso.Web.Flow.Rules
{
    public class RulesManager : IRulesManager
    {
        public RulesManager(IRulesRepository repository, IOptions<ApplicationOptions> optionsAccessor, IRulesCache rulesCache)
        {
            this._repository = repository;
            this._applicationOptions = optionsAccessor.Value;
            this._rulesCache = rulesCache;
        }

        #region Constants
        private const string PIPE = "|";
        #endregion Constants

        #region Fields
        private IRulesRepository _repository;
        private ApplicationOptions _applicationOptions;
        private IRulesCache _rulesCache;
        #endregion Fields

        #region Methods
        public async Task LoadSelectedModules(List<string> modules)
        {
            if (this._rulesCache == null)
                throw new InvalidOperationException(Properties.Resources.rulesCacheCannotBeNull);

            if (modules.Count < 1)
                return;

            List<string> queryItems = modules.Select(item => string.Concat(item, PIPE, this._applicationOptions.ApplicationName)).ToList();
            ICollection<RulesModuleModel> rulesModules = (await this._repository.GetItemsAsync<RulesModuleModel, RulesModule>(item => queryItems.Contains(item.NamePlusApplication)));

            this._rulesCache = rulesModules.Aggregate(this._rulesCache, (cache, next) =>
            {
                string moduleName = next.Name.ToLowerInvariant();
                lock (cache)
                {
                    RuleSet ruleSet = next.DeserializeRuleSetFile();
                    if (ruleSet == null)
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Properties.Resources.invalidRulesetFormat, moduleName));

                    if (cache.RuleEngines.ContainsKey(moduleName))
                        cache.RuleEngines[moduleName] = new RuleEngine(ruleSet, RulesSerializer.GetValidation(ruleSet));
                    else
                        cache.RuleEngines.Add(moduleName, new RuleEngine(ruleSet, RulesSerializer.GetValidation(ruleSet)));

                    using (IResourceReader reader = new ResourceReader(new MemoryStream(next.ResourceSetFile)))
                    {
                        reader.OfType<DictionaryEntry>()
                        .ToList()
                        .ForEach(entry =>
                        {
                            string resourceKey = (string)entry.Key;
                            if (cache.ResourceStrings.ContainsKey(resourceKey))
                                cache.ResourceStrings[resourceKey] = (string)entry.Value;
                            else
                                cache.ResourceStrings.Add(resourceKey, (string)entry.Value);
                        });
                    }
                }

                return cache;
            });

        }
        #endregion Methods
    }
}
