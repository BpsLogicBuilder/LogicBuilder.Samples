using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Data.Entities;
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
    public class SaveDepartmentTest
    {
        public SaveDepartmentTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private IHttpClientFactory clientFactory;
        #endregion Fields

        [Fact]
        public async void SaveDepartment()
        {
            var departmentResponse = await this.clientFactory.PostAsync<GetEntityResponse>
            (
                "api/Entity/GetEntity",
                JsonSerializer.Serialize
                (
                    new Business.Requests.GetEntityRequest
                    {
                        Filter = GetFilterExpressionDescriptor<DepartmentModel>
                        (
                            GetDepartmentByIdFilterBody(2),
                            "q"
                        ),
                        ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                        DataType = typeof(Department).AssemblyQualifiedName
                    }
                )
            );

            DepartmentModel model = (DepartmentModel)departmentResponse.Entity;
            model.Budget = 100001.00m;
            model.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            List<Task<SaveEntityResponse>> tasks = new List<Task<SaveEntityResponse>>();
            for (int i = 0; i < 1; i++)//department returns a rowversion so can only save one.
            {
                tasks.Add
                (
                    this.clientFactory.PostAsync<SaveEntityResponse>
                    (
                        "api/Department/Save",
                        JsonSerializer.Serialize
                        (
                            new SaveEntityRequest
                            {
                                Entity = model
                            }
                        ),
                        "http://localhost:7878/"
                    )
                );

                await Task.WhenAll(tasks);

                tasks.ForEach(task => Assert.True(task.Result.Success));
            }
        }

        private EqualsBinaryOperatorDescriptor GetDepartmentByIdFilterBody(int id)
            => new EqualsBinaryOperatorDescriptor
            {
                Left = new MemberSelectorOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    MemberFullName = "DepartmentID"
                },
                Right = new ConstantOperatorDescriptor { Type = typeof(int).FullName, ConstantValue = 2 }
            };

        private FilterLambdaOperatorDescriptor GetFilterExpressionDescriptor<T>(OperatorDescriptorBase filterBody, string parameterName = "$it")
            => new FilterLambdaOperatorDescriptor
            {
                FilterBody = filterBody,
                SourceElementType = typeof(T).AssemblyQualifiedName,
                ParameterName = parameterName
            };

        private void Initialize()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient();
            serviceProvider = services.BuildServiceProvider();

            this.clientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }
    }
}
