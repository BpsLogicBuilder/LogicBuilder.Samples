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
    [Route("api/Residency")]
    public class ResidencyController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public ResidencyController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deleteResidencyRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Residency/Delete",
                JsonSerializer.Serialize(deleteResidencyRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<BaseResponse> Save([FromBody] SaveEntityRequest saveResidencyRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Residency/Save",
                JsonSerializer.Serialize(saveResidencyRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
