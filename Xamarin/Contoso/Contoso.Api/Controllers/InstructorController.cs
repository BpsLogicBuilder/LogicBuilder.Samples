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
    [Route("api/Instructor")]
    public class InstructorController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public InstructorController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deleteInstructorRequest)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Instructor/Delete",
                JsonSerializer.Serialize(deleteInstructorRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public Task<BaseResponse> Save([FromBody] SaveEntityRequest saveInstructorRequest)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Instructor/Save",
                JsonSerializer.Serialize(saveInstructorRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
