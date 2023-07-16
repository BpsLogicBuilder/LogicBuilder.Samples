using Enrollment.Domain.Entities;
using System.Threading.Tasks;

namespace Enrollment.Spa.Flow.Rules
{
    public interface IRulesLoader
    {
        Task LoadRulesOnStartUp(RulesModuleModel modules, RulesCache cache);
    }
}
