using LogicBuilder.RulesDirector;
using LogicBuilder.Workflow.Activities.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow.Rules
{
    public class RulesCache : IRulesCache
    {
        public RulesCache(Dictionary<string, RuleEngine> ruleEngines, Dictionary<string, string> resourceStrings)
        {
            _ruleEngines = ruleEngines;
            _resourceStrings = resourceStrings;
        }

        private readonly Dictionary<string, RuleEngine> _ruleEngines;
        private readonly Dictionary<string, string> _resourceStrings;

        #region Properties
        public IDictionary<string, RuleEngine> RuleEngines => _ruleEngines;
        public IDictionary<string, string> ResourceStrings => _resourceStrings;
        #endregion Properties

        #region Methods
        public RuleEngine GetRuleEngine(string ruleSet)
        {
            if (this.RuleEngines == null)
                return null;

            return this.RuleEngines.TryGetValue(ruleSet.ToLowerInvariant(), out RuleEngine ruleEngine) ? ruleEngine : null;
        }
        #endregion Methods
    }
}
