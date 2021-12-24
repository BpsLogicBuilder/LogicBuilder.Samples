using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.Contexts;
using Contoso.Kemdo.AutoMapperProfiles;
using Contoso.Repositories;
using Contoso.Stores;
using Contoso.Utils;
using Contoso.Web.Flow;
using Contoso.Web.Flow.Cache;
using Contoso.Web.Flow.Options;
using Contoso.Web.Flow.Rules;
using LogicBuilder.RulesDirector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace Contoso.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCaseExceptDictionaryKeysResolver();
            });

            services.AddDbContext<RulesContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRulesStore, RulesStore>()
                .AddScoped<IRulesRepository, RulesRepository>()
                .AddScoped<ISchoolStore, SchoolStore>()
                .AddScoped<ISchoolRepository, SchoolRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(typeof(SchoolProfile).Assembly);
                        cfg.AddMaps(typeof(GroupingProfile).Assembly);
                        cfg.AddProfile<ExpansionParameterToViewMappingProfile>();
                        cfg.AddProfile<ExpansionViewToOperatorMappingProfile>();
                        cfg.AllowNullCollections = true;
                    })
                )
                .AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddScoped<IRulesLoader, RulesLoader>()
                .AddScoped<IRulesManager, RulesManager>()
                .AddScoped<IFlowManager, FlowManager>()
                .AddScoped<FlowActivityFactory, FlowActivityFactory>()
                .AddScoped<DirectorFactory, DirectorFactory>()
                .AddScoped<FlowDataCache, FlowDataCache>()
                .AddScoped<ICustomActions, CustomActions>()
                .AddScoped<ICustomDialogs, CustomDialogs>()
                .AddSingleton<IRulesCache>(sp =>
                {
                    return (sp.GetRequiredService<IRulesLoader>().LoadRulesOnStartUp()).Result;
                });

            services.Configure<InitialOptions>(Configuration);
            services.Configure<ApplicationOptions>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }

            // Runs matching. An endpoint is selected and set on the HttpContext if a match is found.
            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            // Executes the endpoint that was selected by routing.
            app.UseEndpoints(endpoints =>
            {
                // Mapping of endpoints goes here:
                endpoints.MapControllers();
                //endpoints.MapRazorPages();
            });
        }
    }
}
