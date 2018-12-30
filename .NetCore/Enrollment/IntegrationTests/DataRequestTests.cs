using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.Contexts;
using Enrollment.Data.Entities;
using Enrollment.Domain;
using Enrollment.Domain.Entities;
using Enrollment.Kemdo.AutoMapperProfiles;
using Enrollment.Kendo.ViewModels;
using Enrollment.Repositories;
using Enrollment.Stores;
using Enrollment.Web.Utils;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace IntegrationTests
{
    public class DataRequestTests
    {
        public DataRequestTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        #region Tests
        [Fact]
        public void Get_lookUps_ungrouped_with_aggregates()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = "listName-count~value-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Enrollment.Domain.Entities.LookUpsModel",
                DataType = "Enrollment.Data.Entities.LookUps",
                Includes = null,
                Selects = null,
                Distinct = false
            };

            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository)).Result;

            Assert.Equal(466, result.Total);
            Assert.Equal(5, ((IEnumerable<LookUpsModel>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(466, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_lookUps_grouped_with_aggregates()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = "listName-count~value-min",
                    Filter = null,
                    Group = "listName-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = "Enrollment.Domain.Entities.LookUpsModel",
                DataType = "Enrollment.Data.Entities.LookUps",
                Includes = null,
                Selects = null,
                Distinct = false
            };

            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository)).Result;

            Assert.Equal(466, result.Total);
            Assert.Equal(2, ((IEnumerable<AggregateFunctionsGroupModel<LookUpsModel>>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(466, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_residency_ungrouped_with_aggregates_and_includes()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = "citizenshipStatus-count~countryOfCitizenship-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = "citizenshipStatus-asc",
                    PageSize = 5
                },
                ModelType = "Enrollment.Domain.Entities.ResidencyModel",
                DataType = "Enrollment.Data.Entities.Residency",
                Includes = new string[] { "StatesLivedIn" },
                Selects = null,
                Distinct = false
            };

            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            DataSourceResult result = Task.Run(() => request.InvokeGenericMethod<DataSourceResult>("GetData", repository)).Result;

            Assert.Equal(2, result.Total);
            Assert.Equal(2, ((IEnumerable<ResidencyModel>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("AA", ((IEnumerable<ResidencyModel>)result.Data).First().CountryOfCitizenship);
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(2, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_single_user_with_navigation_property_of_navigation_property()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = null,
                    Filter = "userID~eq~1",
                    Group = null,
                    Page = 0,
                    Sort = null,
                    PageSize = 0
                },
                ModelType = "Enrollment.Domain.Entities.UserModel",
                DataType = "Enrollment.Data.Entities.User",
                Includes = new string[] { "residency.statesLivedIn" },
                Selects = null,
                Distinct = false
            };

            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            UserModel result = (UserModel)Task.Run(() => request.InvokeGenericMethod<BaseModelClass>("GetSingle", repository)).Result;

            Assert.Equal(2, result.Residency.StatesLivedIn.Count());
            Assert.Equal("ForeignStudent01", result.UserName);
        }

        [Fact]
        public void Get_lookup_list_with_select_new()
        {
            DataRequest request = new DataRequest
            {
                Options = new DataSourceRequestOptions
                {
                    Aggregate = null,
                    Filter = "listName~eq~'residentstates'",
                    Group = null,
                    Page = 0,
                    Sort = "value-asc",
                    PageSize = 0
                },
                ModelType = "Enrollment.Domain.Entities.LookUpsModel",
                DataType = "Enrollment.Data.Entities.LookUps",
                Includes = null,
                Selects = new Dictionary<string, string> { ["value"] = "value", ["text"] = "text", ["listName"] = "listName" },
                Distinct = false
            };

            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            IEnumerable<dynamic> result = Task.Run(() => request.InvokeGenericMethod<IEnumerable<dynamic>>("GetDynamicSelect", repository)).Result;

            Assert.Equal("AK", result.First().value);
            Assert.Equal(60, result.Count());
        }
        #endregion Tests

        #region Methods
        private void Initialize()
        {
            serviceProvider = new ServiceCollection()
                .AddDbContext<EnrollmentContext>
                (
                    options =>
                    {
                        options.UseInMemoryDatabase("Enrollment");
                        options.UseInternalServiceProvider(new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider());
                    }
                )
                .AddTransient<IEnrollmentStore, EnrollmentStore>()
                .AddTransient<IEnrollmentRepository, EnrollmentRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfiles(typeof(EnrollmentProfile).GetTypeInfo().Assembly);
                        cfg.AddProfiles(typeof(GroupingProfile).GetTypeInfo().Assembly);
                    })
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            EnrollmentContext context = serviceProvider.GetRequiredService<EnrollmentContext>();
            context.Database.EnsureCreated();

            Task.Run(async () => await Seed_Database(serviceProvider.GetRequiredService<IEnrollmentRepository>())).Wait();
        }
        #endregion Methods

        #region Seed DB
        private static async Task Seed_Database(IEnrollmentRepository repository)
        {
            if ((await repository.CountAsync<LookUpsModel, LookUps>()) > 0)
                return;//database has been seeded

            XmlDocument xDoc = Path.Combine(Directory.GetCurrentDirectory(), "DropDowns.xml").LoadXmlDocument();

            IList<LookUpsModel> lookUps = xDoc.SelectNodes("//list")
                .OfType<XmlElement>()
                .SelectMany
                (
                    e => e.ChildNodes.OfType<XmlElement>()
                    .Where(c => c.Name == "item")
                    .Select
                    (
                        i => new LookUpsModel
                        {
                            ListName = e.Attributes["id"].Value,
                            EntityState = LogicBuilder.Domain.EntityStateType.Added,
                            Value = i.Attributes["name"].Value,
                            Text = i.Attributes["value"].Value,
                            Order = 0
                        }
                    )
                ).ToList();

            await repository.SaveGraphsAsync<LookUpsModel, LookUps>(lookUps);

            UserModel[] users = new UserModel[]
            {
                new UserModel
                {
                    UserName = "ForeignStudent01",
                    Residency = new ResidencyModel
                    {
                        CitizenshipStatus = "US",
                        CountryOfCitizenship = "US",
                        DriversLicenseNumber = "NC12345",
                        DriversLicenseState = "NC",
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        HasValidDriversLicense = true,
                        ImmigrationStatus = "AA",
                        ResidentState = "AR",
                        StatesLivedIn = new List<StateLivedInModel>
                        {
                            new StateLivedInModel { EntityState = LogicBuilder.Domain.EntityStateType.Added, State = "OH"  },
                            new StateLivedInModel { EntityState = LogicBuilder.Domain.EntityStateType.Added, State = "TN"  }
                        }
                    },
                    EntityState = LogicBuilder.Domain.EntityStateType.Added
                },
                new UserModel
                {
                    UserName = "DomesticStudent01",
                    Residency = new ResidencyModel
                    {
                        CitizenshipStatus = "RA",
                        CountryOfCitizenship = "AA",
                        DriversLicenseNumber = "GA12345",
                        DriversLicenseState = "DA",
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        HasValidDriversLicense = true,
                        ImmigrationStatus = "BB",
                        ResidentState = "AR",
                        StatesLivedIn = new List<StateLivedInModel>
                        {
                            new StateLivedInModel { EntityState = LogicBuilder.Domain.EntityStateType.Added, State = "GA"  },
                            new StateLivedInModel { EntityState = LogicBuilder.Domain.EntityStateType.Added, State = "TN" }
                        }
                    },
                    EntityState = LogicBuilder.Domain.EntityStateType.Added
                }
            };

            await repository.SaveGraphsAsync<UserModel, User>(users);
        }
        #endregion Seed DB
    }
}
