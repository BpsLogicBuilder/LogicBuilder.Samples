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

namespace Contoso.Bsl.Flow.Integration.Tests.Rules
{
    public class RulesVsNoRulesDurationTest
    {
        public RulesVsNoRulesDurationTest(Xunit.Abstractions.ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly Xunit.Abstractions.ITestOutputHelper output;
        #endregion Fields

        #region Tests
        [Fact]
        public void SaveStudentRequestWithEnrollments1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();

            //arrange
            flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>
            (
                s => s.FullName == "Carson Alexander",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            student.Enrollments.ToList().ForEach(enrollment =>
            {
                enrollment.Grade = Domain.Entities.Grade.A;
                enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            });
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = student };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("comparisontest");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid student using rules = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Equal("First", ((StudentModel)((SaveEntityResponse)flowManager.FlowDataCache.Response).Entity).FirstName);
            Assert.Equal(Domain.Entities.Grade.A, ((StudentModel)((SaveEntityResponse)flowManager.FlowDataCache.Response).Entity).Enrollments.First().Grade);
        }

        [Fact]
        public void SaveStudentRequestWithEnrollments2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();

            //arrange
            flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>
            (
                s => s.FullName == "Carson Alexander",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            student.Enrollments.ToList().ForEach(enrollment =>
            {
                enrollment.Grade = Domain.Entities.Grade.A;
                enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            });
            flowManager.FlowDataCache.Request = new SaveEntityRequest { Entity = student };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("comparisontest");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid student using rules = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Equal("First", ((StudentModel)((SaveEntityResponse)flowManager.FlowDataCache.Response).Entity).FirstName);
            Assert.Equal(Domain.Entities.Grade.A, ((StudentModel)((SaveEntityResponse)flowManager.FlowDataCache.Response).Entity).Enrollments.First().Grade);
        }

        [Fact]
        public void SaveStudentRequestWithEnrollmentsWithoutRules1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            ISchoolRepository schoolRepository = serviceProvider.GetRequiredService<ISchoolRepository>();
            var student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>
            (
                s => s.FullName == "Carson Alexander",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            student.Enrollments.ToList().ForEach(enrollment =>
            {
                enrollment.Grade = Domain.Entities.Grade.A;
                enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            });
            SaveEntityRequest saveStudentRequest = new SaveEntityRequest { Entity = student };

            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            StudentModel studentModel = (StudentModel)saveStudentRequest.Entity;
            SaveEntityResponse saveStudentResponse = new SaveEntityResponse();
            saveStudentResponse.Success = schoolRepository.SaveGraphAsync<StudentModel, Student>(studentModel).Result;

            if (!saveStudentResponse.Success) return;

            studentModel = schoolRepository.GetAsync<StudentModel, Student>
            (
                f => f.ID == studentModel.ID,
                null,
                new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.SingleOrDefault();

            saveStudentResponse.Entity = studentModel;

            int Iteration_Index = 0;

            flowManager.CustomActions.WriteToLog
            (
                string.Format
                (
                    "EnrollmentCount: {0}. Index: {1}",
                    new object[]
                    {
                        studentModel.Enrollments.Count,
                        Iteration_Index
                    }
                )
            );

            EnrollmentModel enrollmentModel = null;
            while (Iteration_Index < studentModel.Enrollments.Count)
            {
                enrollmentModel = studentModel.Enrollments.ElementAt(Iteration_Index);
                Iteration_Index = Iteration_Index + 1;
                flowManager.CustomActions.WriteToLog
                (
                    string.Format
                    (
                        "Student Id:{0} is enrolled in {1}.",
                        new object[]
                        {
                            studentModel.ID,
                            enrollmentModel.CourseTitle
                        }
                    )
                );
            }

            stopWatch.Stop();
            this.output.WriteLine("WithoutRulesUsingExpressiionsInCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void SaveStudentRequestWithEnrollmentsWithoutRules2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            ISchoolRepository schoolRepository = serviceProvider.GetRequiredService<ISchoolRepository>();
            var student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>
            (
                s => s.FullName == "Carson Alexander",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            student.Enrollments.ToList().ForEach(enrollment =>
            {
                enrollment.Grade = Domain.Entities.Grade.A;
                enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            });
            SaveEntityRequest saveStudentRequest = new SaveEntityRequest { Entity = student };

            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            StudentModel studentModel = (StudentModel)saveStudentRequest.Entity;
            SaveEntityResponse saveStudentResponse = new SaveEntityResponse();
            saveStudentResponse.Success = schoolRepository.SaveGraphAsync<StudentModel, Student>(studentModel).Result;

            if (!saveStudentResponse.Success) return;

            studentModel = schoolRepository.GetAsync<StudentModel, Student>
            (
                f => f.ID == studentModel.ID,
                null,
                new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.SingleOrDefault();

            saveStudentResponse.Entity = studentModel;

            int Iteration_Index = 0;

            flowManager.CustomActions.WriteToLog
            (
                string.Format
                (
                    "EnrollmentCount: {0}. Index: {1}",
                    new object[]
                    {
                        studentModel.Enrollments.Count,
                        Iteration_Index
                    }
                )
            );

            EnrollmentModel enrollmentModel = null;
            while (Iteration_Index < studentModel.Enrollments.Count)
            {
                enrollmentModel = studentModel.Enrollments.ElementAt(Iteration_Index);
                Iteration_Index = Iteration_Index + 1;
                flowManager.CustomActions.WriteToLog
                (
                    string.Format
                    (
                        "Student Id:{0} is enrolled in {1}.",
                        new object[]
                        {
                            studentModel.ID,
                            enrollmentModel.CourseTitle
                        }
                    )
                );
            }

            stopWatch.Stop();
            this.output.WriteLine("WithoutRulesUsingExpressiionsInCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithRules1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Response = new SaveEntityResponse();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("justloop");
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithRules2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Response = new SaveEntityResponse();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("justloop");
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithRulesNoBoxing1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Response = new SaveEntityResponse();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("justloopnoboxing");
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithRulesNoBoxing2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Response = new SaveEntityResponse();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("justloopnoboxing");
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithCode1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.FlowDataCache.Items["Iteration_Index"] = 0;
            while ((int)flowManager.FlowDataCache.Items["Iteration_Index"] < 1000000)
            {
                flowManager.FlowDataCache.Items["Iteration_Index"] = (int)flowManager.FlowDataCache.Items["Iteration_Index"] + 1;
            }
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithCode2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.FlowDataCache.Items["Iteration_Index"] = 0;
            while ((int)flowManager.FlowDataCache.Items["Iteration_Index"] < 1000000)
            {
                flowManager.FlowDataCache.Items["Iteration_Index"] = (int)flowManager.FlowDataCache.Items["Iteration_Index"] + 1;
            }
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithCodeNoBoxing1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.FlowDataCache.Index1 = 0;
            while (flowManager.FlowDataCache.Index1 < 1000000)
            {
                flowManager.FlowDataCache.Index1 = flowManager.FlowDataCache.Index1 + 1;
            }
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithCodeNoBoxing2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.FlowDataCache.Index1 = 0;
            while (flowManager.FlowDataCache.Index1 < 1000000)
            {
                flowManager.FlowDataCache.Index1 = flowManager.FlowDataCache.Index1 + 1;
            }
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
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
                        @"Server=(localdb)\mssqllocaldb;Database=RulesVsNoRulesDurationTest;ConnectRetryCount=0"
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
