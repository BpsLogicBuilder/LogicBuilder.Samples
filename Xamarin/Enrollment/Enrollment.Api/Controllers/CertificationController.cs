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
    [Route("api/Certification")]
    public class CertificationController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public CertificationController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deleteCertificationRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Certification/Delete",
                JsonSerializer.Serialize(deleteCertificationRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<BaseResponse> Save([FromBody] SaveEntityRequest saveCertificationRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Certification/Save",
                JsonSerializer.Serialize(saveCertificationRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
