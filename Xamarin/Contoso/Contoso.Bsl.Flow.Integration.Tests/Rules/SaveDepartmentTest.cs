using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Flow.Cache;
using Contoso.Bsl.Flow.Services;
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Contoso.Bsl.Flow.Integration.Tests.Rules
{
    public class SaveDepartmentTest
    {
        public SaveDepartmentTest(ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly ITestOutputHelper output;
        #endregion Fields

        #region Tests
        [Fact]
        public void SaveDepartment1()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var department = flowManager.SchoolRepository.GetAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {//Need expansion because RowVersion is not a literal type (included without explicit expansion)
                     //Or use GetItemsAsync which does not use projection.
                     //Todo include check for typeof(byte[]) in LogicBuilder.Expressions.Utils.TypeExtension.GetValueTypeMembers()
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem
                        {
                            MemberName = "RowVersion"
                        }
                    }
                }
            ).Result.Single();
            department.Budget = 1000.1m;
            department.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = department };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savedepartment");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid department  = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Equal(1000.1m, ((DepartmentModel)((SaveEntityResponse)flowManager.FlowDataCache.Response).Entity).Budget);
        }

        [Fact]
        public void SaveDepartment2()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var department = flowManager.SchoolRepository.GetItemsAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics"
            ).Result.Single();
            department.Budget = 1000.1m;
            department.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = department };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savedepartment");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid department  = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Equal(1000.1m, ((DepartmentModel)((SaveEntityResponse)flowManager.FlowDataCache.Response).Entity).Budget);
        }

        [Fact]
        public void SaveInvalidSaveDepartment1()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var department = flowManager.SchoolRepository.GetAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {//Need expansion because RowVersion is not a literal type (included without explicit expansion)
                     //Or use GetItemsAsync which does not use projection.
                     //Todo include check for typeof(byte[]) in LogicBuilder.Expressions.Utils.TypeExtension.GetValueTypeMembers()
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem
                        {
                            MemberName = "RowVersion"
                        }
                    }
                }
            ).Result.Single();
            department.DepartmentID = 0;
            department.InstructorID = null;
            department.Budget = -1m;
            department.Name = "";
            department.StartDate = default;
            department.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = department };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savedepartment");
            stopWatch.Stop();
            this.output.WriteLine("Saving invalid department  = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.False(flowManager.FlowDataCache.Response.Success);
        }

        [Fact]
        public void SaveInvalidSaveDepartment2()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var department = flowManager.SchoolRepository.GetItemsAsync<DepartmentModel, Department>
            (
                s => s.Name == "Mathematics"
            ).Result.Single();
            department.DepartmentID = 0;
            department.InstructorID = null;
            department.Budget = -1m;
            department.Name = "";
            department.StartDate = default;
            department.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = department };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savedepartment");
            stopWatch.Stop();
            this.output.WriteLine("Saving invalid department  = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.False(flowManager.FlowDataCache.Response.Success);
        }
        #endregion Tests

        #region Helpers
        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            if (MapperConfiguration == null)
            {
                MapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();

                    cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<SchoolProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=SaveDepartmentTest;ConnectRetryCount=0"
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
                .AddTransient<IGetItemFilterBuilder, GetItemFilterBuilder>()
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
