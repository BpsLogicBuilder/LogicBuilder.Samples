﻿using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.AutoMapperProfiles;
using Enrollment.Bsl.Business.Requests;
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
    public class DeleteContactInfoTest
    {
        public DeleteContactInfoTest(ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly ITestOutputHelper output;
        #endregion Fields

        [Fact]
        public async void DeleteValidContactInfoRequest()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var contactInfo = (await flowManager.EnrollmentRepository.GetAsync<ContactInfoModel, ContactInfo>
            (
                s => s.UserId == 1
            )).Single();
            flowManager.FlowDataCache.Request = new DeleteEntityRequest { Entity = contactInfo };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("deletecontactInfo");
            stopWatch.Stop();
            this.output.WriteLine("Deleting valid contactInfo = {0}", stopWatch.Elapsed.TotalMilliseconds);

            contactInfo = (await flowManager.EnrollmentRepository.GetAsync<ContactInfoModel, ContactInfo>
            (
                s => s.UserId == 1
            )).SingleOrDefault();

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Null(contactInfo);
        }

        [Fact]
        public async void DeleteContactInfoNotFoundRequest()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var contactInfo = (await flowManager.EnrollmentRepository.GetAsync<ContactInfoModel, ContactInfo>
            (
                s => s.UserId == 1
            )).Single();
            contactInfo.UserId = Int32.MaxValue;
            flowManager.FlowDataCache.Request = new DeleteEntityRequest { Entity = contactInfo };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("deletecontactInfo");
            stopWatch.Stop();
            this.output.WriteLine("Deleting contactInfo not found = {0}", stopWatch.Elapsed.TotalMilliseconds);

            contactInfo = (await flowManager.EnrollmentRepository.GetAsync<ContactInfoModel, ContactInfo>
            (
                s => s.UserId == 1
            )).SingleOrDefault();

            //assert
            Assert.False(flowManager.FlowDataCache.Response.Success);
            Assert.Single(flowManager.FlowDataCache.Response.ErrorMessages);
            Assert.NotNull(contactInfo);
        }

        #region Helpers
        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            MapperConfiguration ??= new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();

                    cfg.AddMaps(typeof(DescriptorToOperatorMappingProfile), typeof(EnrollmentProfile));
                });
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<EnrollmentContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=DeleteContactInfoTest;ConnectRetryCount=0"
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
