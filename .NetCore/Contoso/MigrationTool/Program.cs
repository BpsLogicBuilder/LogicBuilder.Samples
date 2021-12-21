using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace MigrationTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (MigrationContext context = new MigrationContext())
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
        }
    }
}
