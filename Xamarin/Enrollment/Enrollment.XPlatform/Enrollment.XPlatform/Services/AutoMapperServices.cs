using AutoMapper;
using Enrollment.AutoMapperProfiles;
using Enrollment.XPlatform.AutoMapperProfiles;
using Enrollment.XPlatform.MappingProfiles;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class AutoMapperServices
    {
        internal static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            return services.AddSingleton<IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(typeof(DescriptorToOperatorMappingProfile), typeof(CommandButtonProfile), typeof(MeuItemProfile));
                        cfg.AllowNullCollections = true;
                    })
                ).AddTransient<IMapper>
                (
                    sp => new Mapper
                    (
                        sp.GetRequiredService<AutoMapper.IConfigurationProvider>(),
                        sp.GetService
                    )
                );
        }
    }
}
