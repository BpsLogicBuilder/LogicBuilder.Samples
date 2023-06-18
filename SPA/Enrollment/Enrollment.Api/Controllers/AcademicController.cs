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
    [Route("api/Academic")]
    public class AcademicController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public AcademicController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deleteAcademicRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Academic/Delete",
                JsonSerializer.Serialize(deleteAcademicRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<BaseResponse> Save([FromBody] SaveEntityRequest saveAcademicRequest)
            => await this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Academic/Save",
                JsonSerializer.Serialize(saveAcademicRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
