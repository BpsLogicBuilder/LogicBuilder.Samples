﻿using Enrollment.Bsl.Business.Requests;
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
    public class AnonymousTypeListController : ControllerBase
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public AnonymousTypeListController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("GetList")]
        public Task<BaseResponse> GetList([FromBody] GetObjectListRequest request)
            => this.clientFactory.PostAsync<BaseResponse>
            (
                "api/AnonymousTypeList/GetList",
                JsonSerializer.Serialize(request),
                this.configurationOptions.BaseBslUrl
            );
    }
}
