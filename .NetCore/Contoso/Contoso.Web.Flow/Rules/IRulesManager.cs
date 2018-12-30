using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Web.Flow.Rules
{
    public interface IRulesManager
    {
        Task LoadSelectedModules(List<string> modules);
    }
}
