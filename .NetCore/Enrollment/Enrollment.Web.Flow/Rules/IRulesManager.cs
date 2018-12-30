using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment.Web.Flow.Rules
{
    public interface IRulesManager
    {
        Task LoadSelectedModules(List<string> modules);
    }
}
