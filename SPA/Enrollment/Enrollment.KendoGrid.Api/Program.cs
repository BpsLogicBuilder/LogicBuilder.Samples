
using Enrollment.Common.Configuration.Json;
using Enrollment.Domain.Json;
using Enrollment.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace Enrollment.KendoGrid.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Enrollment.KendoGrid.Api", Version = "v1" });
            });

            services.Configure<ConfigurationOptions>(configuration);
            services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Enrollment.KendoGrid.Api v1"));
            }

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}