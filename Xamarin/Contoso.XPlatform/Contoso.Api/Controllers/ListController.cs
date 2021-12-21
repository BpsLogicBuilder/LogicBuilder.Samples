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
    [Route("api/List")]
    public class ListController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public ListController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("GetList")]
        public Task<BaseResponse> GetList([FromBody] GetTypedListRequest request)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/List/GetList",
                JsonSerializer.Serialize(request),
                this.configurationOptions.BaseBslUrl
            );
    }
}
