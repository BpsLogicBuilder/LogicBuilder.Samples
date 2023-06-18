using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.AutoMapperProfiles;
using Enrollment.BSL.AutoMapperProfiles;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
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
                    Aggregate = "lastName-count~zipCode-min",
                    Filter = null,
                    Group = null,
                    Page = 1,
                    Sort = "zipCode-asc",
                    PageSize = 5
                },
                ModelType = typeof(PersonalModel).AssemblyQualifiedName,
                DataType = typeof(Personal).AssemblyQualifiedName
            };

            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = Task.Run(() => request.GetData(repository, mapper)).Result;

            Assert.Equal(2, result.Total);
            Assert.Equal(2, ((IEnumerable<PersonalModel>)result.Data).Count());
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(2, (int)result.AggregateResults.First().Value);
        }

        [Fact]
        public void Get_persons_grouped_with_aggregates()
        {
            KendoGridDataRequest request = new()
            {
                Options = new KendoGridDataSourceRequestOptions
                {
                    Aggregate = "lastName-count~zipCode-min",
                    Filter = null,
                    Group = "zipCode-asc",
                    Page = 1,
                    Sort = null,
                    PageSize = 5
                },
                ModelType = typeof(PersonalModel).AssemblyQualifiedName,
                DataType = typeof(Personal).AssemblyQualifiedName
            };

            IEnrollmentRepository repository = serviceProvider.GetRequiredService<IEnrollmentRepository>();
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            DataSourceResult result = Task.Run(() => request.GetData(repository, mapper)).Result;

            Assert.Equal(2, result.Total);
            Assert.Single((IEnumerable<AggregateFunctionsGroup>)result.Data);
            Assert.Equal(2, result.AggregateResults.Count());
            Assert.Equal("Count", result.AggregateResults.First().AggregateMethodName);
            Assert.Equal(2, (int)result.AggregateResults.First().Value);
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
                cfg.AddProfile<EnrollmentProfile>();
                cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
            });
        }

        [MemberNotNull(nameof(serviceProvider))]
        private void Initialize()
        {
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<EnrollmentContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=RequestHelpersTest;ConnectRetryCount=0"
                    ),
                    ServiceLifetime.Transient
                )
                .AddTransient<IEnrollmentStore, EnrollmentStore>()
                .AddTransient<IEnrollmentRepository, EnrollmentRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            EnrollmentContext context = serviceProvider.GetRequiredService<EnrollmentContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed_Database(serviceProvider.GetRequiredService<IEnrollmentRepository>()).Wait();
        }
        #endregion Helpers

        #region Seed DB
        private static async Task Seed_Database(IEnrollmentRepository repository)
        {
            if ((await repository.CountAsync<LookUpsModel, LookUps>()) > 0)
                return;//database has been seeded

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Path.Combine(Directory.GetCurrentDirectory(), "DropDowns.xml"));

            IList<LookUpsModel> lookUps = xDoc.SelectNodes("//list")
                .OfType<XmlElement>()
                .SelectMany
                (
                    e => e.ChildNodes.OfType<XmlElement>()
                    .Where(c => c.Name == "item")
                    .Select
                    (
                        i =>
                        {
                            if (new HashSet<string> { "isVeteran", "receivedGed", "creditHoursAtCmc", "yesNo" }.Contains(e.Attributes["id"].Value))
                                return new LookUpsModel
                                {
                                    ListName = e.Attributes["id"].Value,
                                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                                    BooleanValue = bool.Parse(i.Attributes["name"].Value),
                                    Text = i.Attributes["value"].Value,
                                    Order = 0
                                };
                            else
                                return new LookUpsModel
                                {
                                    ListName = e.Attributes["id"].Value,
                                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                                    Value = i.Attributes["name"].Value,
                                    Text = i.Attributes["value"].Value,
                                    Order = 0
                                };
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
                    Academic = new AcademicModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        AttendedPriorColleges = true,
                        FromDate = new DateTime(2010, 10, 10),
                        ToDate = new DateTime(2014, 10, 10),
                        GraduationStatus = "H",
                        EarnedCreditAtCmc = true,
                        LastHighSchoolLocation = "NC",
                        NcHighSchoolName = "NCSCHOOL1",
                        Institutions = new List<InstitutionModel>
                        {
                            new InstitutionModel
                            {
                                EntityState = LogicBuilder.Domain.EntityStateType.Added,
                                HighestDegreeEarned = "BD",
                                StartYear = "2015",
                                EndYear = "2018",
                                InstitutionName = "I1",
                                InstitutionState = "floridaInstitutions",
                                MonthYearGraduated = new DateTime(2020, 10, 10)
                            }
                        }
                    },
                    Admissions = new AdmissionsModel
                    {
                        EnteringStatus = "1",
                        EnrollmentTerm = "FA",
                        EnrollmentYear = "2021",
                        ProgramType = "degreePrograms",
                        Program = "degreeProgram1",
                        EntityState = LogicBuilder.Domain.EntityStateType.Added
                    },
                    Certification = new CertificationModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        CertificateStatementChecked = true,
                        DeclarationStatementChecked = true,
                        PolicyStatementsChecked = true
                    },
                    ContactInfo = new ContactInfoModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        HasFormerName = true,
                        FormerFirstName = "John",
                        FormerMiddleName = "Michael",
                        FormerLastName = "Smith",
                        DateOfBirth = new DateTime(2003, 10, 10),
                        SocialSecurityNumber = "111-22-3333",
                        Gender = "M",
                        Race = "AN",
                        Ethnicity = "HIS",
                        EnergencyContactFirstName = "Jackson",
                        EnergencyContactLastName = "Zamarano",
                        EnergencyContactRelationship = "Father",
                        EnergencyContactPhoneNumber = "704-333-4444"
                    },
                    MoreInfo = new MoreInfoModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        ReasonForAttending = "C1",
                        OverallEducationalGoal = "E1",
                        IsVeteran = true,
                        MilitaryStatus = "A",
                        VeteranType = "H",
                        MilitaryBranch = "AF"
                    },
                    Personal = new PersonalModel
                    {
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
                        DriversLicenseState = "GA",
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
                    Academic = new AcademicModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        AttendedPriorColleges = true,
                        FromDate = new DateTime(2010, 10, 10),
                        ToDate = new DateTime(2014, 10, 10),
                        GraduationStatus = "CSD",
                        EarnedCreditAtCmc = false,
                        LastHighSchoolLocation = "NC",
                        NcHighSchoolName = "NCSCHOOL1",
                        Institutions = new List<InstitutionModel>
                        {
                            new InstitutionModel
                            {
                                EntityState = LogicBuilder.Domain.EntityStateType.Added,
                                HighestDegreeEarned = "CT",
                                StartYear = "2016",
                                EndYear = "2019",
                                InstitutionName = "I1",
                                InstitutionState = "floridaInstitutions",
                                MonthYearGraduated = new DateTime(2020, 10, 10)
                            }
                        }
                    },
                    Admissions = new AdmissionsModel
                    {
                        EnteringStatus = "1",
                        EnrollmentTerm = "FA",
                        EnrollmentYear = "2021",
                        ProgramType = "degreePrograms",
                        Program = "degreeProgram1",
                        EntityState = LogicBuilder.Domain.EntityStateType.Added
                    },
                    Certification = new CertificationModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        CertificateStatementChecked = true,
                        DeclarationStatementChecked = true,
                        PolicyStatementsChecked = true
                    },
                    ContactInfo = new ContactInfoModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        HasFormerName = false,
                        DateOfBirth = new DateTime(2003, 10, 10),
                        SocialSecurityNumber = "000-11-2222",
                        Gender = "F",
                        Race = "BL",
                        Ethnicity = "NHS",
                        EnergencyContactFirstName = "Jack",
                        EnergencyContactLastName = "Spratt",
                        EnergencyContactRelationship = "Father",
                        EnergencyContactPhoneNumber = "704-222-3333"
                    },
                    MoreInfo = new MoreInfoModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        ReasonForAttending = "C2",
                        OverallEducationalGoal = "E2",
                        IsVeteran = true,
                        MilitaryStatus = "A",
                        VeteranType = "G",
                        MilitaryBranch = "Army"
                    },
                    Personal = new PersonalModel
                    {
                        EntityState = LogicBuilder.Domain.EntityStateType.Added,
                        FirstName = "Mike",
                        MiddleName = "Tyson",
                        LastName = "Smith",
                        PrimaryEmail = "go.stay@jack.com",
                        Address1 = "Second Street",
                        City = "Dallas",
                        State = "GA",
                        ZipCode = "30060",
                        CellPhone = "770-855-0050",
                    },
                    EntityState = LogicBuilder.Domain.EntityStateType.Added
                }
            };

            await repository.SaveGraphsAsync<UserModel, User>(users);
        }
        #endregion Seed DB
    }
}
