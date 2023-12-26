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
    public class SaveAcademicTest
    {
        public SaveAcademicTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        private const string BASE_URL = "http://localhost:62419/";
        #endregion Fields

        [Fact]
        public async void SaveAcademic()
        {
            List<Task<SaveEntityResponse>> tasks = [];
            for (int i = 0; i < 30; i++)
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<SaveEntityResponse>
                    (
                        "api/Academic/Save",
                        JsonSerializer.Serialize
                        (
                            new SaveEntityRequest
                            {
                                Entity = new AcademicModel
                                {
                                    UserId = 1,
                                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                                    AttendedPriorColleges = true,
                                    FromDate = new DateTime(2010, 10, 11),
                                    ToDate = new DateTime(2014, 10, 10),
                                    GraduationStatus = "CSD",
                                    EarnedCreditAtCmc = false,
                                    LastHighSchoolLocation = "NC",
                                    NcHighSchoolName = "NCSCHOOL1",
                                    Institutions = new List<InstitutionModel>
                                    {
                                        new() {
                                            InstitutionId = 1,
                                            UserId = 1,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                                            HighestDegreeEarned = "CT",
                                            StartYear = "2017",
                                            EndYear = "2020",
                                            InstitutionName = "I1",
                                            InstitutionState = "floridaInstitutions",
                                            MonthYearGraduated = new DateTime(2020, 10, 10)
                                        }
                                    }
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
