using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.Contexts;
using Enrollment.Data.Entities;
using Enrollment.Domain.Entities;
using Enrollment.Repositories;
using Enrollment.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace IntegrationTests
{
    public class RepositoryTests
    {
        public RepositoryTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

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
                .AddSingleton<AutoMapper.IConfigurationProvider>(new MapperConfiguration(cfg => cfg.AddProfiles(typeof(EnrollmentProfile).GetTypeInfo().Assembly)))
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            EnrollmentContext context = serviceProvider.GetRequiredService<EnrollmentContext>();
            context.Database.EnsureCreated();

            Task.Run(async () => await Seed_Database(serviceProvider.GetRequiredService<IEnrollmentRepository>())).Wait();
        }
        #endregion Methods

        #region Tests
        [Fact]
        public void Get_students_with_no_includes()
        {
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            ICollection<UserModel> list = Task.Run(() => repository.GetItemsAsync<UserModel, User>()).Result;

            Assert.Equal(2, list.Count);
            Assert.Null(list.First().Residency);
        }

        [Fact]
        public void Get_students_inlude_navigation_property()
        {
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            ICollection<UserModel> list = Task.Run(() => repository.GetItemsAsync<UserModel, User>(s => s.Residency.StatesLivedIn.Count() > 0, null,
                    new Expression<Func<IQueryable<UserModel>, IIncludableQueryable<UserModel, object>>>[]
                    {
                        a => a.Include(x => x.Residency)
                    })).Result;

            Assert.NotNull(list.First().Residency);
            Assert.True(list.First().Residency.StatesLivedIn == null || list.First().Residency.StatesLivedIn.Count() == 0);
        }

        [Fact]
        public void Get_students_inlude_navigation_property_of_navigation_property()
        {
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            ICollection<UserModel> list = Task.Run(() => repository.GetItemsAsync<UserModel, User>(s => s.Residency.StatesLivedIn.Count() > 0, null,
                    new Expression<Func<IQueryable<UserModel>, IIncludableQueryable<UserModel, object>>>[]
                    {
                        a => a.Include(x => x.Residency).ThenInclude(e => e.StatesLivedIn)
                    })).Result.ToList();

            Assert.NotNull(list.First().Residency);
            Assert.NotNull(list.First().Residency.StatesLivedIn.First().State);
        }

        [Fact]
        public void Get_students_filter_by_navigation_property_of_navigation_property()
        {
            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            ICollection<UserModel> list = Task.Run(() => repository.GetItemsAsync<UserModel, User>(s => s.Residency.StatesLivedIn.Any(e => e.State == "GA"))).Result.ToList();

            Assert.True(list.Count == 1);
        }
        #endregion Tests

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
