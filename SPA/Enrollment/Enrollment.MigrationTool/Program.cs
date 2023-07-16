using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Enrollment.MigrationTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IConfigurationRoot config = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .Build();

            //IServiceProvider serviceProvider = new ServiceCollection().AddDbContext<MigrationContext>(options =>
            //    options.UseSqlServer(config.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient)
            //    .BuildServiceProvider();

            using MigrationContext context = new();
            context.Database.Migrate();
            context.Database.EnsureCreated();
        }
    }
}
