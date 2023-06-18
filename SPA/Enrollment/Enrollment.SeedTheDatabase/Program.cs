using AutoMapper;
using Enrollment.BSL.AutoMapperProfiles;
using Enrollment.Contexts;
using Enrollment.Data.Entities;
using Enrollment.Domain;
using Enrollment.Domain.Entities;
using Enrollment.Repositories;
using Enrollment.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Enrollment.SeedTheDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            IServiceProvider serviceProvider = new ServiceCollection().AddDbContext<EnrollmentContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient)
                .AddTransient<IEnrollmentStore, EnrollmentStore>()
                .AddTransient<IEnrollmentRepository, EnrollmentRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>(new MapperConfiguration(cfg => cfg.AddMaps(typeof(MyProfile).GetTypeInfo().Assembly)))
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            EnrollmentContext context = serviceProvider.GetRequiredService<EnrollmentContext>();
            context.Database.EnsureCreated();

            Task.Run(async () => await Seed_Database(serviceProvider.GetRequiredService<IEnrollmentRepository>())).Wait();
        }

        private static async Task Seed_Database(IEnrollmentRepository repository)
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
    }
}