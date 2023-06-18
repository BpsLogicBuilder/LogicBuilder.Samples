using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Domain.Entities;
using Enrollment.Web.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Enrollment.Api.Web.Tests
{
    public class SaveMoreInfoTest
    {
        public SaveMoreInfoTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        private const string BASE_URL = "http://localhost:7878/";
        #endregion Fields

        [Fact]
        public async void SaveMoreInfo()
        {
            List<Task<SaveEntityResponse>> tasks = new List<Task<SaveEntityResponse>>();
            for (int i = 0; i < 30; i++)
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<SaveEntityResponse>
                    (
                        "api/MoreInfo/Save",
                        JsonSerializer.Serialize
                        (
                            new SaveEntityRequest
                            {
                                Entity = new MoreInfoModel
                                {
                                    UserId = 1,
                                    ReasonForAttending = "C2",
                                    OverallEducationalGoal = "E3",
                                    IsVeteran = true,
                                    MilitaryStatus = "A",
                                    VeteranType = "G",
                                    MilitaryBranch = "Army",
                                    EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                }
                            }
                        ),
                        BASE_URL
                    )
                );

                await Task.WhenAll(tasks);

                tasks.ForEach(task => Assert.True(task.Result.Success));
            }
        }

        private void Initialize()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            serviceProvider = services.BuildServiceProvider();

            this.clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }
    }
}
