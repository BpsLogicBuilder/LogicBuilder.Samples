using Enrollment.Contexts;
using Enrollment.Data.Entities;
using Enrollment.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Xunit;

namespace IntegrationTests
{
    public class StoreTests
    {
        public StoreTests()
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
                ).AddTransient<IEnrollmentStore, EnrollmentStore>().BuildServiceProvider();

            EnrollmentContext context = serviceProvider.GetRequiredService<EnrollmentContext>();
            context.Database.EnsureCreated();

            Task.Run(async () => await Seed_Database(serviceProvider.GetRequiredService<IEnrollmentStore>())).Wait();
        }
        #endregion Methods

        #region Tests
        [Fact]
        public void Get_students_with_no_includes()
        {
            IEnrollmentStore store = serviceProvider.GetRequiredService<IEnrollmentStore>();
            ICollection<User> list = Task.Run(() => store.GetAsync<User>()).Result;

            Assert.Equal(2, list.Count);
            Assert.Null(list.First().Residency);
        }

        [Fact]
        public void Get_students_inlude_navigation_property()
        {
            IEnrollmentStore store = serviceProvider.GetRequiredService<IEnrollmentStore>();
            ICollection<User> list = Task.Run(() => store.GetAsync<User>(s => s.Residency.StatesLivedIn.Count() > 0, null,
                    new Func<IQueryable<User>, IIncludableQueryable<User, object>>[]
                    {
                        a => a.Include(x => x.Residency)
                    })).Result;

            Assert.NotNull(list.First().Residency);
            Assert.Null(list.First().Residency.StatesLivedIn);
        }

        [Fact]
        public void Get_students_inlude_navigation_property_of_navigation_property()
        {
            IEnrollmentStore store = serviceProvider.GetRequiredService<IEnrollmentStore>();
            ICollection<User> list = Task.Run(() => store.GetAsync<User>(s => s.Residency.StatesLivedIn.Count() > 0, null,
                    new Func<IQueryable<User>, IIncludableQueryable<User, object>>[]
                    {
                        a => a.Include(x => x.Residency).ThenInclude(e => e.StatesLivedIn)
                    })).Result.ToList();

            Assert.NotNull(list.First().Residency);
            Assert.NotNull(list.First().Residency.StatesLivedIn.First().State);
        }

        [Fact]
        public void Get_students_filter_by_navigation_property_of_navigation_property()
        {
            IEnrollmentStore store = serviceProvider.GetRequiredService<IEnrollmentStore>();
            ICollection<User> list = Task.Run(() => store.GetAsync<User>(s => s.Residency.StatesLivedIn.Any(e => e.State == "GA"))).Result.ToList();

            Assert.True(list.Count == 1);
        }
        #endregion Tests

        #region Seed DB
        private static async Task Seed_Database(IEnrollmentStore store)
        {
            if ((await store.CountAsync<LookUps>()) > 0)
                return;//database has been seeded

            XmlDocument xDoc = Path.Combine(Directory.GetCurrentDirectory(), "DropDowns.xml").LoadXmlDocument();

            IList<LookUps> lookUps = xDoc.SelectNodes("//list")
                .OfType<XmlElement>()
                .SelectMany
                (
                    e => e.ChildNodes.OfType<XmlElement>()
                    .Where(c => c.Name == "item")
                    .Select
                    (
                        i => new LookUps
                        {
                            ListName = e.Attributes["id"].Value,
                            EntityState  = LogicBuilder.Data.EntityStateType.Added,
                            Value = i.Attributes["name"].Value,
                            Text = i.Attributes["value"].Value,
                            Order = 0
                        }
                    )
                ).ToList();

            await store.SaveGraphsAsync<LookUps>(lookUps);

            User[] users = new User[]
            {
                new User
                {
                    UserName = "ForeignStudent01",
                    Residency = new Residency
                    {
                        CitizenshipStatus = "US",
                        CountryOfCitizenship = "US",
                        DriversLicenseNumber = "NC12345",
                        DriversLicenseState = "NC",
                        EntityState = LogicBuilder.Data.EntityStateType.Added,
                        HasValidDriversLicense = true,
                        ImmigrationStatus = "AA",
                        ResidentState = "AR",
                        StatesLivedIn = new List<StateLivedIn>
                        {
                            new StateLivedIn { EntityState = LogicBuilder.Data.EntityStateType.Added, State = "OH"  },
                            new StateLivedIn { EntityState = LogicBuilder.Data.EntityStateType.Added, State = "TN"  }
                        }
                    },
                    EntityState = LogicBuilder.Data.EntityStateType.Added
                },
                new User
                {
                    UserName = "DomesticStudent01",
                    Residency = new Residency
                    {
                        CitizenshipStatus = "RA",
                        CountryOfCitizenship = "AA",
                        DriversLicenseNumber = "GA12345",
                        DriversLicenseState = "DA",
                        EntityState = LogicBuilder.Data.EntityStateType.Added,
                        HasValidDriversLicense = true,
                        ImmigrationStatus = "BB",
                        ResidentState = "AR",
                        StatesLivedIn = new List<StateLivedIn>
                        {
                            new StateLivedIn { EntityState = LogicBuilder.Data.EntityStateType.Added, State = "GA"  },
                            new StateLivedIn { EntityState = LogicBuilder.Data.EntityStateType.Added, State = "TN" }
                        }
                    },
                    EntityState = LogicBuilder.Data.EntityStateType.Added
                }
            };

            await store.SaveGraphsAsync<User>(users);
        }
        #endregion Seed DB
    }
}
