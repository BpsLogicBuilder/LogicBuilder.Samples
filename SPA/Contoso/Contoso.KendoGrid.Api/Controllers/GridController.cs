using Contoso.KendoGrid.Bsl.Business.Requests;
using Contoso.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Contoso.KendoGrid.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GridController : ControllerBase
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public GridController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("GetData")]
        public async Task<System.Text.Json.Nodes.JsonObject> GetData([FromBody] KendoGridDataRequest request)
        {
            JsonObject res = await this.clientFactory.PostAsync<JsonObject>
            (
                "api/Grid/GetData",
                JsonSerializer.Serialize(request),
                this.configurationOptions.BaseBslUrl
            );
            return res;
        }
    }
}
