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
    public class SaveResidencyTest
    {
        public SaveResidencyTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        private const string BASE_URL = "http://localhost:62419/";
        #endregion Fields

        [Fact]
        public async void SaveResidency()
        {
            List<Task<SaveEntityResponse>> tasks = new List<Task<SaveEntityResponse>>();
            for (int i = 0; i < 30; i++)
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<SaveEntityResponse>
                    (
                        "api/Residency/Save",
                        JsonSerializer.Serialize
                        (
                            new SaveEntityRequest
                            {
                                Entity = new ResidencyModel
                                {
                                    UserId = 1,
                                    CitizenshipStatus = "RA",
                                    CountryOfCitizenship = "AA",
                                    DriversLicenseNumber = "GA12345",
                                    DriversLicenseState = "GA",
                                    HasValidDriversLicense = true,
                                    ImmigrationStatus = "BB",
                                    ResidentState = "AR",
                                    StatesLivedIn = new List<StateLivedInModel>
                                    {
                                        new StateLivedInModel { StateLivedInId = 1, UserId = 1, EntityState = LogicBuilder.Domain.EntityStateType.Modified, State = "GA"  },
                                        new StateLivedInModel { StateLivedInId = 2, UserId = 1, EntityState = LogicBuilder.Domain.EntityStateType.Modified, State = "TN" }
                                    },
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
