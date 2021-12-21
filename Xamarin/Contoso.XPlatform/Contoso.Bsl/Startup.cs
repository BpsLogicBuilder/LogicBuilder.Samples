using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Bsl.Flow;
using Contoso.Bsl.Flow.Cache;
using Contoso.Bsl.Flow.Services;
using Contoso.BSL.AutoMapperProfiles;
using Contoso.Common.Configuration.Json;
using Contoso.Contexts;
using Contoso.Domain.Json;
using Contoso.Repositories;
using Contoso.Stores;
using Contoso.Utils;
using LogicBuilder.RulesDirector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace Contoso.Bsl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            System.Collections.Generic.List<System.Reflection.Assembly> assemblies = new System.Collections.Generic.List<System.Reflection.Assembly>
            {
                typeof(Parameters.Expansions.SelectExpandDefinitionParameters).Assembly,
                typeof(Contoso.Utils.TypeHelpers).Assembly,
                typeof(Domain.BaseModelClass).Assembly,
                typeof(Data.BaseDataClass).Assembly,
                typeof(DirectorBase).Assembly,
                typeof(string).Assembly
            };

            rulesCache = Bsl.Flow.Rules.RulesService.LoadRules().Result;
        }

        public IConfiguration Configuration { get; }
        private readonly IRulesCache rulesCache;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contoso.Bsl", Version = "v1" });
            })
            .AddDbContext<SchoolContext>
            (
                options => options.UseSqlServer
                (
                    Configuration.GetConnectionString("DefaultConnection")
                )
            )
            .AddScoped<ISchoolStore, SchoolStore>()
            .AddScoped<ISchoolRepository, SchoolRepository>()
            .AddSingleton<AutoMapper.IConfigurationProvider>
            (
                new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();

                    cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<SchoolProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                })
            )
            //.AddSingleton<AutoMapper.IConfigurationProvider>(sp => 
            //{
            //    const string mapperConfigurationKey = "mapperConfiguration";
            //    //IMemoryCache cache = sp.GetRequiredService<IMemoryCache>();
            //    if (!cache.TryGetValue<AutoMapper.IConfigurationProvider>(mapperConfigurationKey, out AutoMapper.IConfigurationProvider config))
            //    {

            //        config = new MapperConfiguration(cfg =>
            //        {
            //            cfg.AddExpressionMapping();

            //            cfg.AddProfile<ParameterToDescriptorMappingProfile>();
            //            cfg.AddProfile<DescriptorToOperatorMappingProfile>();
            //            cfg.AddProfile<SchoolProfile>();
            //            cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
            //            cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
            //        });

            //        cache.Set(mapperConfigurationKey, config, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(1) });
            //    }

            //    return config;
            //})
            .AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
            .AddScoped<IFlowManager, FlowManager>()
            .AddScoped<FlowActivityFactory, FlowActivityFactory>()
            .AddScoped<DirectorFactory, DirectorFactory>()
            .AddScoped<ICustomActions, CustomActions>()
            //.AddMemoryCache()
            .AddScoped<FlowDataCache, FlowDataCache>()
            .AddScoped<Progress, Progress>()
            .AddScoped<IGetItemFilterBuilder, GetItemFilterBuilder>()
            .AddSingleton<IRulesCache>(sp =>
            {
                //const string rulesKey = "rules";
                //IMemoryCache cache = sp.GetRequiredService<IMemoryCache>();
                //ILogger<Startup> logger = sp.GetRequiredService<ILogger<Startup>>();
                //if (!cache.TryGetValue<IRulesCache>(rulesKey, out IRulesCache _rulesCache))
                //{
                    
                //    //long before = GC.GetTotalMemory(false);
                //    _rulesCache = rulesCache;
                //    //long after = GC.GetTotalMemory(false);
                //    //long size = after - before;

                //    logger.LogInformation($"Setting rules cache: {DateTimeOffset.Now}");
                //    cache.Set(rulesKey, _rulesCache, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(1) });
                //}
                //else
                //{
                //    logger.LogInformation($"Found rules cache: {DateTimeOffset.Now}");
                //}

                return rulesCache;
                //return rulesCache;
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contoso.Bsl v1"));
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
