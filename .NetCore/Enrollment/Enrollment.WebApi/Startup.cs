using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.Contexts;
using Enrollment.Kemdo.AutoMapperProfiles;
using Enrollment.Repositories;
using Enrollment.Stores;
using Enrollment.Web.Flow;
using Enrollment.Web.Flow.Cache;
using Enrollment.Web.Flow.Options;
using Enrollment.Web.Flow.Rules;
using LogicBuilder.RulesDirector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            services.AddMvc();

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
                        cfg.AddProfiles(typeof(EnrollmentProfile).Assembly);
                        cfg.AddProfiles(typeof(GroupingProfile).Assembly);
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseMvc();
        }
    }
}
