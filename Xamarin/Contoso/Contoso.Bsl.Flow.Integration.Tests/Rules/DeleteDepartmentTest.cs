using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Flow.Cache;
using Contoso.BSL.AutoMapperProfiles;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Repositories;
using Contoso.Stores;
using LogicBuilder.RulesDirector;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Contoso.Bsl.Flow.Integration.Tests.Rules
{
    public class DeleteDepartmentTest
    {
        public DeleteDepartmentTest(ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly ITestOutputHelper output;
        #endregion Fields

        [Fact]
        public async void DeleteValidDepartmentRequest()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var department = (await flowManager.SchoolRepository.GetAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics"
            )).Single();
            flowManager.FlowDataCache.Request = new DeleteEntityRequest { Entity = department };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("deletedepartment");
            stopWatch.Stop();
            this.output.WriteLine("Deleting valid department = {0}", stopWatch.Elapsed.TotalMilliseconds);

            department = (await flowManager.SchoolRepository.GetAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics"
            )).SingleOrDefault();

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Null(department);
        }

        [Fact]
        public async void DeleteInvalidDepartmentRequest()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var department = (await flowManager.SchoolRepository.GetAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics"
            )).Single();
            department.Name = "";
            flowManager.FlowDataCache.Request = new DeleteEntityRequest { Entity = department };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("deletedepartment");
            stopWatch.Stop();
            this.output.WriteLine("Deleting invalid department = {0}", stopWatch.Elapsed.TotalMilliseconds);

            department = (await flowManager.SchoolRepository.GetAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics"
            )).SingleOrDefault();

            //assert
            Assert.False(flowManager.FlowDataCache.Response.Success);
            Assert.Single(flowManager.FlowDataCache.Response.ErrorMessages);
            Assert.NotNull(department);
        }

        [Fact]
        public async void DeleteDepartmentNotFoundRequest()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var department = (await flowManager.SchoolRepository.GetAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics"
            )).Single();
            department.DepartmentID = Int32.MaxValue;
            flowManager.FlowDataCache.Request = new DeleteEntityRequest { Entity = department };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("deletedepartment");
            stopWatch.Stop();
            this.output.WriteLine("Deleting department not found = {0}", stopWatch.Elapsed.TotalMilliseconds);

            department = (await flowManager.SchoolRepository.GetAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics"
            )).SingleOrDefault();

            //assert
            Assert.False(flowManager.FlowDataCache.Response.Success);
            Assert.Single(flowManager.FlowDataCache.Response.ErrorMessages);
            Assert.NotNull(department);
        }

        #region Helpers
        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            MapperConfiguration ??= new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();

                    cfg.AddMaps(typeof(DescriptorToOperatorMappingProfile), typeof(SchoolProfile));
                });
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=DeleteDepartmentTest;ConnectRetryCount=0"
                    ),
                    ServiceLifetime.Transient
                )
                .AddLogging
                (
                    loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.Services.AddSingleton<ILoggerProvider>
                        (
                            serviceProvider => new XUnitLoggerProvider(this.output)
                        );
                        loggingBuilder.AddFilter<XUnitLoggerProvider>("*", LogLevel.None);
                        loggingBuilder.AddFilter<XUnitLoggerProvider>("Contoso.Bsl.Flow", LogLevel.Trace);
                    }
                )
                .AddTransient<ISchoolStore, SchoolStore>()
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddTransient<IFlowManager, FlowManager>()
                .AddTransient<FlowActivityFactory, FlowActivityFactory>()
                .AddTransient<DirectorFactory, DirectorFactory>()
                .AddTransient<ICustomActions, CustomActions>()
                .AddSingleton<FlowDataCache, FlowDataCache>()
                .AddSingleton<Progress, Progress>()
                .AddSingleton<IRulesCache>(sp =>
                {
                    return Bsl.Flow.Rules.RulesService.LoadRules().GetAwaiter().GetResult();
                })
                .BuildServiceProvider();

            ReCreateDataBase(serviceProvider.GetRequiredService<SchoolContext>());
            DatabaseSeeder.Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>()).Wait();
        }

        private static void ReCreateDataBase(SchoolContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        #endregion Helpers
    }
}
