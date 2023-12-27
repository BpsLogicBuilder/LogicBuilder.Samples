using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Domain.Entities;
using Contoso.Web.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Api.Web.Tests
{
    public class SaveStudentTest
    {
        public SaveStudentTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        #endregion Fields

        #region Tests
        [Fact]
        public async void SaveStudent()
        {
            List<Task<BaseResponse>> tasks = [];
            for (int i = 0; i < 30; i++)
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<BaseResponse>
                    (
                        "api/Student/Save",
                        JsonSerializer.Serialize
                        (
                            new SaveEntityRequest
                            {
                                Entity = new StudentModel
                                {
                                    ID = 1,
                                    FirstName = "Carson",
                                    LastName = "Alexander",
                                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                                    Enrollments = new HashSet<EnrollmentModel>
                                    {
                                        new() {
                                            EnrollmentID = 1,
                                            CourseID = 1050,
                                            Grade = Contoso.Domain.Entities.Grade.A,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new() {
                                            EnrollmentID = 2,
                                            CourseID = 4022,
                                            Grade = Contoso.Domain.Entities.Grade.C,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new() {
                                            EnrollmentID = 3,
                                            CourseID = 4041,
                                            Grade = Contoso.Domain.Entities.Grade.B,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        }
                                    }
                                }
                            }
                        ),
                        "http://localhost:7878/"
                    )
                );

                var results = (await Task.WhenAll(tasks)).ToList();

                results.ForEach(result =>
                {
                    Assert.True(result.Success);
                    Assert.True(result is SaveEntityResponse saveEntityResponse);
                    Assert.NotNull(((SaveEntityResponse)result).Entity as StudentModel);
                });
            }
        }

        [Fact]
        public async void SaveStudentWithoutRules()
        {
            List<Task<SaveEntityResponse>> tasks = [];
            for (int i = 0; i < 30; i++)
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<SaveEntityResponse>
                    (
                        "api/Student/SaveWithoutRules",
                        JsonSerializer.Serialize
                        (
                            new SaveEntityRequest
                            {
                                Entity = new StudentModel
                                {
                                    ID = 1,
                                    FirstName = "Carson",
                                    LastName = "Alexander",
                                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                                    Enrollments = new HashSet<EnrollmentModel>
                                    {
                                        new() {
                                            EnrollmentID = 1,
                                            CourseID = 1050,
                                            Grade = Contoso.Domain.Entities.Grade.A,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new() {
                                            EnrollmentID = 2,
                                            CourseID = 4022,
                                            Grade = Contoso.Domain.Entities.Grade.C,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new() {
                                            EnrollmentID = 3,
                                            CourseID = 4041,
                                            Grade = Contoso.Domain.Entities.Grade.B,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        }
                                    }
                                }
                            }
                        ),
                        Constants.BASE_URL
                    )
                );

                var results = (await Task.WhenAll(tasks)).ToList();

                results.ForEach(result => Assert.True(result.Success));
            }
        }
        #endregion Tests

        private void Initialize()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            serviceProvider = services.BuildServiceProvider();

            this.clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }
    }
}
