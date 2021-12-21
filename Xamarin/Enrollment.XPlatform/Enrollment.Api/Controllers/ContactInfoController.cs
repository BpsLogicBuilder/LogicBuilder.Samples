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
    [Route("api/ContactInfo")]
    public class ContactInfoController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public ContactInfoController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deleteContactInfoRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/ContactInfo/Delete",
                JsonSerializer.Serialize(deleteContactInfoRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<BaseResponse> Save([FromBody] SaveEntityRequest saveContactInfoRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/ContactInfo/Save",
                JsonSerializer.Serialize(saveContactInfoRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
