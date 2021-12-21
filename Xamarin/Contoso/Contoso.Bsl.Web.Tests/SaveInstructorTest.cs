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
    public class SaveInstructorTest
    {
        public SaveInstructorTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        #endregion Fields

        [Fact]
        public async void SaveInstructor()
        {
            List<Task<SaveEntityResponse>> tasks = new List<Task<SaveEntityResponse>>();
            for (int i = 0; i < 30; i++)
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<SaveEntityResponse>
                    (
                        "api/Instructor/Save",
                        JsonSerializer.Serialize
                        (
                            new SaveEntityRequest
                            {
                                Entity = new InstructorModel 
                                { 
                                    ID = 3,
                                    FirstName = "Fadi", 
                                    LastName = "Fakhouri", 
                                    HireDate = DateTime.Parse("2002-07-07"), 
                                    OfficeAssignment = new OfficeAssignmentModel { Location = "Smith 17", EntityState = LogicBuilder.Domain.EntityStateType.Modified }, 
                                    Courses = new List<CourseAssignmentModel>
                                    {
                                        new CourseAssignmentModel { CourseID = 1045, InstructorID = 3, EntityState = LogicBuilder.Domain.EntityStateType.Unchanged }
                                    },
                                    EntityState = LogicBuilder.Domain.EntityStateType.Modified
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

        private void Initialize()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            serviceProvider = services.BuildServiceProvider();

            this.clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }
    }
}
