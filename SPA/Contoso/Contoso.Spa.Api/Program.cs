
using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.Common.Configuration.Json;
using Contoso.Domain.Json;
using Contoso.Spa.AutoMapperProfiles;
using Contoso.Spa.Flow;
using Contoso.Spa.Flow.Cache;
using Contoso.Spa.Flow.Options;
using Contoso.Spa.Flow.Rules;
using Contoso.Utils;
using LogicBuilder.RulesDirector;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;

namespace Contoso.Spa.Api
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
            logger.Debug("init main Contoso.Spa.Api");

            try
            {
                IRulesCache rulesCache = RulesService.LoadRules().Result;
                var builder = WebApplication.CreateBuilder(args);

                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.Host.UseNLog();

                IServiceCollection services = builder.Services;

                // Add services to the container.

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
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contoso.Spa.Api", Version = "v1" });
                })
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    new MapperConfiguration(cfg => {
                        cfg.AddMaps(typeof(BaseClassMappings).Assembly);
                        cfg.AddMaps(typeof(ExpansionDescriptorToOperatorMappingProfile).Assembly);
                    })
                )
                .AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddScoped<IRulesLoader, RulesLoader>()
                .AddScoped<IFlowManager, FlowManager>()
                .AddScoped<IFlowActivityFactory, FlowActivityFactory>()
                .AddScoped<IDirectorFactory, DirectorFactory>()
                .AddScoped<FlowDataCache, FlowDataCache>()
                .AddScoped<ICustomActions, CustomActions>()
                .AddScoped<ICustomDialogs, CustomDialogs>()
                .AddSingleton(sp => rulesCache);

                services.Configure<InitialOptions>(configuration);

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contoso.Spa.Api v1"));
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