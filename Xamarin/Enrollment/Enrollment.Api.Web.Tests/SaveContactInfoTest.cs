using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Domain.Entities;
using Enrollment.Web.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Enrollment.Api.Web.Tests
{
    public class SaveContactInfoTest
    {
        public SaveContactInfoTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        private const string BASE_URL = "http://localhost:62419/";
        #endregion Fields

        [Fact]
        public async void SaveContactInfo()
        {
            List<Task<SaveEntityResponse>> tasks = [];
            for (int i = 0; i < 30; i++)
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<SaveEntityResponse>
                    (
                        "api/ContactInfo/Save",
                        JsonSerializer.Serialize
                        (
                            new SaveEntityRequest
                            {
                                Entity = new ContactInfoModel
                                {
                                    UserId = 1,
                                    HasFormerName = false,
                                    DateOfBirth = new DateTime(2003, 10, 10),
                                    SocialSecurityNumber = "000-11-2222",
                                    Gender = "F",
                                    Race = "BL",
                                    Ethnicity = "NHS",
                                    EnergencyContactFirstName = "Jack",
                                    EnergencyContactLastName = "Spratt",
                                    EnergencyContactRelationship = "Father",
                                    EnergencyContactPhoneNumber = "770-222-3333",
                                    EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                }
                            }
                        ),
                        BASE_URL
                    )
                );

                var reults = (await Task.WhenAll(tasks)).ToList();

                reults.ForEach(result => Assert.True(result.Success));
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
