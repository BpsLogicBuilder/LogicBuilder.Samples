using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Enrollment.AutoMapperProfiles;
using Enrollment.BSL.AutoMapperProfiles;
using Enrollment.Common.Configuration.Json;
using Enrollment.Contexts;
using Enrollment.Domain.Json;
using Enrollment.Repositories;
using Enrollment.Stores;
using Enrollment.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System;
using System.IO;

namespace Enrollment.KendoGrid.Bsl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

            NLog.GlobalDiagnosticsContext.Set("DefaultConnection", configuration.GetConnectionString("DefaultConnection"));

            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.Host.UseNLog();

                IServiceCollection services = builder.Services;

                services.AddCors();
                services.AddControllers().AddJsonOptions
                (
                    options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new DescriptorConverter());
                        options.JsonSerializerOptions.Converters.Add(new ModelConverter());
                        options.JsonSerializerOptions.Converters.Add(new ObjectConverter());
                    }
                );

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Enrollment.KendoGrid.Bsl", Version = "v1" });
                })
                .AddDbContext<MyContext>
                (
                    options => options.UseSqlServer
                    (
                        configuration.GetConnectionString("DefaultConnection")
                    )
                )
                .AddScoped<IMyStore, MyStore>()
                .AddScoped<IMyRepository, MyRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddExpressionMapping();

                        cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                        cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                        cfg.AddProfile<MyProfile>();
                        cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                        cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                    })
                )
                .AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enrollment.KendoGrid.Bsl v1"));
                    app.UseExceptionHandler("/error-local-development");
                }
                else
                {
                    app.UseExceptionHandler("/error");
                }

                app.UseRouting();

                app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}