using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.AutoMapperProfiles;
using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Bsl.Flow.Cache;
using Enrollment.BSL.AutoMapperProfiles;
using Enrollment.Contexts;
using Enrollment.Domain.Entities;
using Enrollment.Repositories;
using Enrollment.Stores;
using LogicBuilder.RulesDirector;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Enrollment.Bsl.Flow.Integration.Tests.Rules
{
    public class InsertPersonalTest
    {
        public InsertPersonalTest(ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly ITestOutputHelper output;
        #endregion Fields

        [Fact]
        public void SavePersonal()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var user = new UserModel
            {
                UserName = "NewName",
                EntityState = LogicBuilder.Domain.EntityStateType.Added
            };
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = user };
            flowManager.Start("saveuser");
            Assert.True(user.UserId > 1);

            var personal = new PersonalModel
            {
                UserId = user.UserId,
                EntityState = LogicBuilder.Domain.EntityStateType.Added,
                FirstName = "Michael",
                MiddleName = "Jackson",
                LastName = "Smith",
                PrimaryEmail = "go.here@jack.com",
                Address1 = "First Street",
                City = "Dallas",
                State = "GA",
                ZipCode = "30060",
                CellPhone = "770-840-8756",
            };
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = personal };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savepersonal");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid personal  = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Empty(flowManager.FlowDataCache.Response.ErrorMessages);

            PersonalModel model = (PersonalModel)((SaveEntityResponse)flowManager.FlowDataCache.Response).Entity;
            Assert.Equal("Michael", model.FirstName);
        }

        [Fact]
        public void SaveInvalidPersonal()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var user = new UserModel
            {
                UserName = "NewName",
                EntityState = LogicBuilder.Domain.EntityStateType.Added
            };
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = user };
            flowManager.Start("saveuser");
            Assert.True(user.UserId > 1);

            var personal = new PersonalModel
            {
                UserId = user.UserId,
                EntityState = LogicBuilder.Domain.EntityStateType.Added,
                FirstName = null,
                MiddleName = null,
                LastName = null,
                PrimaryEmail = "123",
                Address1 = null,
                City = null,
                County = null,
                State = "NC",
                ZipCode = "123",
                CellPhone = "123",
                OtherPhone = "123"
            };
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = personal };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savepersonal");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid personal = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.False(flowManager.FlowDataCache.Response.Success);
            Assert.Equal(10, flowManager.FlowDataCache.Response.ErrorMessages.Count);
        }

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
                    cfg.AddProfile<EnrollmentProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<EnrollmentContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=InsertPersonalTest;ConnectRetryCount=0"
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
