using Contoso.Common.Configuration.ExpressionDescriptors;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public interface IPagingSelectorBuilder
    {
        Task<SelectorLambdaOperatorDescriptor> CreateSelector(int skip, string? searchText, string createPagingSelectorFlowName);
    }
}
