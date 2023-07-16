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
    [Route("api/Personal")]
    public class PersonalController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public PersonalController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deletePersonalRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Personal/Delete",
                JsonSerializer.Serialize(deletePersonalRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<BaseResponse> Save([FromBody] SaveEntityRequest savePersonalRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Personal/Save",
                JsonSerializer.Serialize(savePersonalRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
