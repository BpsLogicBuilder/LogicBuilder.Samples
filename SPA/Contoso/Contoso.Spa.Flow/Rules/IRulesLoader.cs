using Contoso.Domain.Entities;
using LogicBuilder.RulesDirector;
using System.Threading.Tasks;

namespace Contoso.Spa.Flow.Rules
{
    public interface IRulesLoader
    {
        Task LoadRulesOnStartUp(RulesModuleModel module, IRulesCache cache);
        Task LoadRules(RulesModuleModel module, IRulesCache cache);
    }
}
