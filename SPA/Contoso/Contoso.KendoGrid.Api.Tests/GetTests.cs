using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.KendoGrid.Bsl.Business.Requests;
using Contoso.Web.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Xunit;

namespace Contoso.KendoGrid.Api.Tests
{
    public class GetTests
    {
        public GetTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        private const string BASE_URL = "http://localhost:12055/";
        #endregion Fields

        #region Helpers
        [MemberNotNull(nameof(clientFactory), nameof(serviceProvider))]
        private void Initialize()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            serviceProvider = services.BuildServiceProvider();

            this.clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }
        #endregion Helpers


        [Fact]
        public async void Get_students_ungrouped_with_aggregates()
        {
            var request = new KendoGridDataRequest
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "lastName-count~enrollmentDate-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = "enrollmentDate-asc",
                    PageSize = 5
                },
                ModelType = typeof(StudentModel).AssemblyQualifiedName,
                DataType = typeof(Student).AssemblyQualifiedName
            };
            var result = await this.clientFactory.PostAsync<System.Text.Json.Nodes.JsonObject>
            (
                "api/Grid/GetData",
                JsonSerializer.Serialize
                (
                    request
                ),
                BASE_URL
            );

            Assert.True(result.Any());
        }
    }
}
