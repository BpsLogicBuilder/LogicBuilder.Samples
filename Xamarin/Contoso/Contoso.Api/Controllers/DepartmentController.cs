using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contoso.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Department")]
    public class DepartmentController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public DepartmentController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deleteDepartmentRequest)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Department/Delete",
                JsonSerializer.Serialize(deleteDepartmentRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public Task<BaseResponse> Save([FromBody] SaveEntityRequest saveDepartmentRequest)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Department/Save",
                JsonSerializer.Serialize(saveDepartmentRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}