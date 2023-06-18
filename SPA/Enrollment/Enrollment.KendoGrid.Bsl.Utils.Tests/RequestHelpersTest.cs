using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.AutoMapperProfiles;
using Enrollment.BSL.AutoMapperProfiles;
using Enrollment.Common.Configuration.ExpansionDescriptors;
using Enrollment.Contexts;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.KendoGrid.Bsl.Business.Requests;
using Enrollment.Repositories;
using Enrollment.Stores;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Enrollment.KendoGrid.Bsl.Utils.Tests
{
    public class RequestHelpersTest
    {
        static RequestHelpersTest()
        {
            InitializeMapperConfiguration();
        }

        public RequestHelpersTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void Get_persons_ungrouped_with_aggregates()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "lastName-count~dateOfBirth-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = "dateOfBirth-asc",
                    PageSize = 5
                },
                ModelType = typeof(PersonModel).FullName,
                DataType = typeof(Person).FullName,
            };

            IMyRepository repository = serviceProvider.GetRequiredService<IMyRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = Task.Run(() => request.GetData(repository, mapper)).Result;

            Assert.Equal(11, result.Total);
            Assert.Equal(5, ((IEnumerable<PersonModel>)result.Data).Count());
            Assert.Equal("Nino", ((IEnumerable<PersonModel>)result.Data).First().FirstName);
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(11, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_persons_grouped_with_aggregates()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "lastName-count~dateOfBirth-min",
                    Filter = null,
                    Group = "dateOfBirth-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = typeof(PersonModel).FullName,
                DataType = typeof(Person).FullName,
            };

            IMyRepository repository = serviceProvider.GetRequiredService<IMyRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = Task.Run(() => request.GetData(repository, mapper)).Result;

            Assert.Equal(11, result.Total);
            Assert.Equal(3, ((IEnumerable<AggregateFunctionsGroup>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(11, (int)result.AggregateResults.First().Value);
        }

        #region Helpers
        static MapperConfiguration MapperConfiguration;

        [MemberNotNull(nameof(MapperConfiguration))]
        private static void InitializeMapperConfiguration()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();

                cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                cfg.AddProfile<MyProfile>();
                cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
            });
        }

        [MemberNotNull(nameof(serviceProvider))]
        private void Initialize()
        {
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<MyContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=RequestHelpersTest;ConnectRetryCount=0"
                    ),
                    ServiceLifetime.Transient
                )
                .AddTransient<IMyStore, MyStore>()
                .AddTransient<IMyRepository, MyRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            MyContext context = serviceProvider.GetRequiredService<MyContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed_Database(serviceProvider.GetRequiredService<IMyRepository>()).Wait();
        }
        #endregion Helpers

        #region Seed DB
        private static async Task Seed_Database(IMyRepository repository)
        {
            if ((await repository.CountAsync<PersonModel, Person>()) > 0)
                return;//database has been seeded

            PersonModel[] persons = new PersonModel[]
            {
                new PersonModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Carson",   LastName = "Alexander",
                    DateOfBirth = DateTime.Parse("2010-09-01")
                },
                new PersonModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Meredith", LastName = "Alonso",
                    DateOfBirth = DateTime.Parse("2012-09-01")
                },
                new PersonModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Arturo",   LastName = "Anand",
                    DateOfBirth = DateTime.Parse("2013-09-01")
                },
                new PersonModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Gytis",    LastName = "Barzdukas",
                    DateOfBirth = DateTime.Parse("2012-09-01")
                },
                new PersonModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Yan",      LastName = "Li",
                    DateOfBirth = DateTime.Parse("2012-09-01")
                },
                new PersonModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Peggy",    LastName = "Justice",
                    DateOfBirth = DateTime.Parse("2011-09-01")
                },
                new PersonModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Laura",    LastName = "Norman",
                    DateOfBirth = DateTime.Parse("2013-09-01")
                },
                new PersonModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Nino",     LastName = "Olivetto",
                    DateOfBirth = DateTime.Parse("2005-09-01")
                },
                new PersonModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Tom",
                    LastName = "Spratt",
                    DateOfBirth = DateTime.Parse("2010-09-01")
                },
                new PersonModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Billie",
                    LastName = "Spratt",
                    DateOfBirth = DateTime.Parse("2010-09-01")
                },
                new PersonModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Jackson",
                    LastName = "Spratt",
                    DateOfBirth = DateTime.Parse("2017-09-01")
                }
            };

            await repository.SaveGraphsAsync<PersonModel, Person>(persons);
        }
        #endregion Seed DB
    }
}
