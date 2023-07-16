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
    [Route("api/Admissions")]
    public class AdmissionsController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public AdmissionsController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deleteAdmissionsRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Admissions/Delete",
                JsonSerializer.Serialize(deleteAdmissionsRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<BaseResponse> Save([FromBody] SaveEntityRequest saveAdmissionsRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Admissions/Save",
                JsonSerializer.Serialize(saveAdmissionsRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
