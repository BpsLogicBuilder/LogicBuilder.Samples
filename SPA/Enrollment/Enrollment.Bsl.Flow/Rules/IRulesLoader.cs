using Enrollment.Domain.Entities;
using LogicBuilder.RulesDirector;
using System.Threading.Tasks;

namespace Enrollment.Bsl.Flow.Rules
{
    public interface IRulesLoader
    {
        Task LoadRulesOnStartUp(RulesModuleModel module, IRulesCache cache);
        Task LoadRules(RulesModuleModel module, IRulesCache cache);
    }
}
