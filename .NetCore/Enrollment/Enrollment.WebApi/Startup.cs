using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.Contexts;
using Enrollment.Kemdo.AutoMapperProfiles;
using Enrollment.Repositories;
using Enrollment.Stores;
using Enrollment.Utils;
using Enrollment.Web.Flow;
using Enrollment.Web.Flow.Cache;
using Enrollment.Web.Flow.Options;
using Enrollment.Web.Flow.Rules;
using LogicBuilder.RulesDirector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Enrollment.WebApi
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
            services.AddDbContext<EnrollmentContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IRulesStore, RulesStore>()
                .AddScoped<IRulesRepository, RulesRepository>()
                .AddScoped<IEnrollmentStore, EnrollmentStore>()
                .AddScoped<IEnrollmentRepository, EnrollmentRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(typeof(EnrollmentProfile).Assembly);
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
                    return sp.GetRequiredService<IRulesLoader>().LoadRulesOnStartUp().Result;
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
