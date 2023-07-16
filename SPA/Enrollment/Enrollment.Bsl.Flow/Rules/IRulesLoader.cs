using Enrollment.Domain.Entities;
using System.Threading.Tasks;

namespace Enrollment.Bsl.Flow.Rules
{
    public interface IRulesLoader
    {
        Task LoadRulesOnStartUp(RulesModuleModel modules, RulesCache cache);
    }
}
