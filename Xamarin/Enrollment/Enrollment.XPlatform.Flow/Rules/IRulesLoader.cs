using Enrollment.Domain.Entities;

namespace Enrollment.XPlatform.Flow.Rules
{
    public interface IRulesLoader
    {
        void LoadRules(RulesModuleModel modules, RulesCache cache);
    }
}
