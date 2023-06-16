using Contoso.Domain.Entities;
using System.Threading.Tasks;

namespace Contoso.Spa.Flow.Rules
{
    public interface IRulesLoader
    {
        Task LoadRulesOnStartUp(RulesModuleModel modules, RulesCache cache);
    }
}
