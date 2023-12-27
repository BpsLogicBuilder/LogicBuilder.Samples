using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.AutoMapperProfiles;
using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Bsl.Flow.Cache;
using Enrollment.BSL.AutoMapperProfiles;
using Enrollment.Contexts;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.Repositories;
using Enrollment.Stores;
using LogicBuilder.RulesDirector;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Enrollment.Bsl.Flow.Integration.Tests.Rules
{
    public class UpdateAcademicTest
    {
        public UpdateAcademicTest(ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly ITestOutputHelper output;
        #endregion Fields

        [Fact]
        public async void SaveAcademic()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var academic = (await flowManager.EnrollmentRepository.GetAsync<AcademicModel, Academic>
            (
                s => s.UserId == 1,
                null,
                new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems =
                    [
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem
                        {
                            MemberName = "Institutions"
                        }
                    ]
                }
            )).Single();

            academic.LastHighSchoolLocation = "FL";
            InstitutionModel institution = academic.Institutions.First();
            institution.EndYear = "2222";
            academic.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            institution.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = academic };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("saveacademic");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid academic = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Empty(flowManager.FlowDataCache.Response.ErrorMessages);

            AcademicModel model = (AcademicModel)((SaveEntityResponse)flowManager.FlowDataCache.Response).Entity;
            Assert.Equal("FL", model.LastHighSchoolLocation);
            Assert.Equal("2222", model.Institutions.First().EndYear);
        }

        [Fact]
        public async void SaveInvalidAcademic()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var academic = (await flowManager.EnrollmentRepository.GetAsync<AcademicModel, Academic>
            (
                s => s.UserId == 1,
                null,
                new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems =
                    [
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem
                        {
                            MemberName = "Institutions"
                        }
                    ]
                }
            )).Single();
            academic.LastHighSchoolLocation = null;
            academic.FromDate = new DateTime();
            academic.ToDate = new DateTime();
            academic.GraduationStatus = null;
            InstitutionModel institution = academic.Institutions.First();
            institution.EndYear = null;
            academic.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            institution.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = academic };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("saveacademic");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid academic = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.False(flowManager.FlowDataCache.Response.Success);
            Assert.Equal(5, flowManager.FlowDataCache.Response.ErrorMessages.Count);
        }

        #region Helpers
        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            MapperConfiguration ??= new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();

                    cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<EnrollmentProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                });
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<EnrollmentContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=SaveAcademicTest;ConnectRetryCount=0"
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
                        loggingBuilder.AddFilter<XUnitLoggerProvider>("Enrollment.Bsl.Flow", LogLevel.Trace);
                    }
                )
                .AddTransient<IEnrollmentStore, EnrollmentStore>()
                .AddTransient<IEnrollmentRepository, EnrollmentRepository>()
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

            ReCreateDataBase(serviceProvider.GetRequiredService<EnrollmentContext>());
            DatabaseSeeder.Seed_Database(serviceProvider.GetRequiredService<IEnrollmentRepository>()).Wait();
        }

        private static void ReCreateDataBase(EnrollmentContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        #endregion Helpers
    }
}
