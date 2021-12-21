using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.AutoMapperProfiles;
using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Bsl.Flow.Cache;
using Enrollment.Bsl.Flow.Services;
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
    public class UpdateContactInfoTest
    {
        public UpdateContactInfoTest(ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly ITestOutputHelper output;
        #endregion Fields

        [Fact]
        public void SaveContactInfo()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var contactInfo = flowManager.EnrollmentRepository.GetAsync<ContactInfoModel, ContactInfo>
            (
                s => s.UserId == 1
            ).Result.Single();

            contactInfo.EnergencyContactFirstName = "Samson";
            contactInfo.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = contactInfo };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savecontactInfo");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid contactInfo  = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Empty(flowManager.FlowDataCache.Response.ErrorMessages);

            ContactInfoModel model = (ContactInfoModel)((SaveEntityResponse)flowManager.FlowDataCache.Response).Entity;
            Assert.Equal("Samson", model.EnergencyContactFirstName);
        }

        [Fact]
        public void SaveInvalidContactInfo()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var contactInfo = flowManager.EnrollmentRepository.GetAsync<ContactInfoModel, ContactInfo>
            (
                s => s.UserId == 1
            ).Result.Single();
            contactInfo.HasFormerName = true;
            contactInfo.FormerFirstName = null;
            contactInfo.FormerLastName = null;
            contactInfo.DateOfBirth = default;
            contactInfo.SocialSecurityNumber = "123";
            contactInfo.Gender = null;
            contactInfo.Race = null;
            contactInfo.Ethnicity = null;
            contactInfo.EnergencyContactFirstName = null;
            contactInfo.EnergencyContactLastName = null;
            contactInfo.EnergencyContactRelationship = null;
            contactInfo.EnergencyContactPhoneNumber= "123";
            contactInfo.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = contactInfo };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savecontactInfo");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid contactInfo = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.False(flowManager.FlowDataCache.Response.Success);
            Assert.Equal(11, flowManager.FlowDataCache.Response.ErrorMessages.Count);
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
                        @"Server=(localdb)\mssqllocaldb;Database=SaveContactInfoTest;ConnectRetryCount=0"
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
                .AddTransient<IGetItemFilterBuilder, GetItemFilterBuilder>()
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
