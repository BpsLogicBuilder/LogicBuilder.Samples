using System.Threading.Tasks;

namespace Contoso.Web.Flow.Rules
{
    public interface IRulesLoader
    {
        Task<RulesCache> LoadRulesOnStartUp();
    }
}
