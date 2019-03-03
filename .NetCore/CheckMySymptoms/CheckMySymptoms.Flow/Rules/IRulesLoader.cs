using CheckMySymptoms.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheckMySymptoms.Flow.Rules
{
    public interface IRulesLoader
    {
        Task<RulesCache> LoadRulesOnStartUp(ICollection<RulesModuleModel> modules);
        Task LoadRulesOnStartUp(RulesModuleModel modules, RulesCache cache);
        void LoadRules(RulesModuleModel modules, RulesCache cache);
    }
}
