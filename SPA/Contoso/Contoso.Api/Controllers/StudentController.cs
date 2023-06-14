using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contoso.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public StudentController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public Task<BaseResponse> Delete([FromBody] DeleteEntityRequest deleteStudentRequest)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Student/Delete",
                JsonSerializer.Serialize(deleteStudentRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public Task<BaseResponse> Save([FromBody] SaveEntityRequest saveStudentRequest) 
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Student/Save",
                JsonSerializer.Serialize(saveStudentRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("SaveWithoutRules")]
        public Task<BaseResponse> SaveWithoutRules([FromBody] SaveEntityRequest saveStudentRequest)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/Student/SaveWithoutRules",
                JsonSerializer.Serialize(saveStudentRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpGet]
        public Task<IEnumerable<string>> Get() 
            => this.clientFactory.GetAsync<IEnumerable<string>>
            (
                "api/Student",
                this.configurationOptions.BaseBslUrl
            );
    }
}
