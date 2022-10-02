using Enrollment.Common.Configuration.ExpressionDescriptors;
using System.Threading.Tasks;

namespace Enrollment.XPlatform.Services
{
    public interface IPagingSelectorBuilder
    {
        Task<SelectorLambdaOperatorDescriptor> CreateSelector(int skip, string? searchText, string createPagingSelectorFlowName);
    }
}
