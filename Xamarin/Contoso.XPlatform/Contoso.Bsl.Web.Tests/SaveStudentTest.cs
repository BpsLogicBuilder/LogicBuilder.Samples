using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Domain.Entities;
using Contoso.Web.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Bsl.Web.Tests
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
            List<Task<BaseResponse>> tasks = new List<Task<BaseResponse>>();
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
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 1,
                                            CourseID = 1050,
                                            Grade = Contoso.Domain.Entities.Grade.A,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 2,
                                            CourseID = 4022,
                                            Grade = Contoso.Domain.Entities.Grade.C,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new EnrollmentModel
                                        {
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

                await Task.WhenAll(tasks);

                tasks.ForEach(task =>
                {
                    Assert.True(task.Result.Success);
                    Assert.True(task.Result is SaveEntityResponse saveEntityResponse);
                    Assert.NotNull(((SaveEntityResponse)task.Result).Entity as StudentModel);
                });
            }
        }

        [Fact]
        public async void SaveStudentWithoutRules()
        {
            List<Task<SaveEntityResponse>> tasks = new List<Task<SaveEntityResponse>>();
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
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 1,
                                            CourseID = 1050,
                                            Grade = Contoso.Domain.Entities.Grade.A,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new EnrollmentModel
                                        {
                                            EnrollmentID = 2,
                                            CourseID = 4022,
                                            Grade = Contoso.Domain.Entities.Grade.C,
                                            EntityState = LogicBuilder.Domain.EntityStateType.Modified
                                        },
                                        new EnrollmentModel
                                        {
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

                await Task.WhenAll(tasks);
                tasks.ForEach(task => Assert.True(task.Result.Success));
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
