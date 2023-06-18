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
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public PersonController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deletePersonRequest)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Person/Delete",
                JsonSerializer.Serialize(deletePersonRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public Task<BaseResponse> Save([FromBody] SaveEntityRequest savePersonRequest)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Person/Save",
                JsonSerializer.Serialize(savePersonRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
