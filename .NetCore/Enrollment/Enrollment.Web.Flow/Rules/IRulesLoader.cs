using System.Threading.Tasks;

namespace Enrollment.Web.Flow.Rules
{
    public interface IRulesLoader
    {
        Task<RulesCache> LoadRulesOnStartUp();
    }
}
