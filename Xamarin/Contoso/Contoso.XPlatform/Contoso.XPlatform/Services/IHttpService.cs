using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public interface IHttpService
    {
        Task<BaseResponse> GetObjectDropDown(GetTypedListRequest request, string url = null);
        Task<BaseResponse> GetList(GetTypedListRequest request, string url = null);
        Task<BaseResponse> GetEntity(GetEntityRequest request, string url = null);
        Task<BaseResponse> SaveEntity(SaveEntityRequest request, string url = null);
        Task<BaseResponse> DeleteEntity(DeleteEntityRequest request, string url = null);
    }
}
