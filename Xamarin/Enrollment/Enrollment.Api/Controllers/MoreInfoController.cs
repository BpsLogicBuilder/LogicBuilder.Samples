using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Enrollment.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/MoreInfo")]
    public class MoreInfoController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public MoreInfoController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deleteMoreInfoRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/MoreInfo/Delete",
                JsonSerializer.Serialize(deleteMoreInfoRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<BaseResponse> Save([FromBody] SaveEntityRequest saveMoreInfoRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/MoreInfo/Save",
                JsonSerializer.Serialize(saveMoreInfoRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
