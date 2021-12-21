using Contoso.Domain.Entities;

namespace Contoso.XPlatform.Flow.Rules
{
    public interface IRulesLoader
    {
        void LoadRules(RulesModuleModel modules, RulesCache cache);
    }
}
