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
            this.RuleEngines = ruleEngines;
            this.ResourceStrings = resourceStrings;
        }

        #region Properties
        public Dictionary<string, RuleEngine> RuleEngines { get; private set; }
        public Dictionary<string, string> ResourceStrings { get; private set; }
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
