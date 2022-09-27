using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using System.Threading.Tasks;

namespace Enrollment.XPlatform.Services
{
    public interface IHttpService
    {
        Task<BaseResponse> GetObjectDropDown(GetTypedListRequest request, string? url = null);
        Task<BaseResponse> GetList(GetTypedListRequest request, string? url = null);
        Task<BaseResponse> GetEntity(GetEntityRequest request, string? url = null);
        Task<BaseResponse> SaveEntity(SaveEntityRequest request, string url);
        Task<BaseResponse> DeleteEntity(DeleteEntityRequest request, string url);
    }
}
