using AutoMapper;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Contoso.XPlatform.ViewModels.Factories;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ReadOnlyServices
    {
        internal static IServiceCollection AddReadOnlyServices(this IServiceCollection services)
        {
            return services
                .AddTransient<Func<Type, List<ItemBindingDescriptor>, ICollectionCellItemsBuilder>>
                (
                    provider =>
                    (modelType, itemBindings) =>
                    {
                        return new CollectionCellItemsBuilder
                        (
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IMapper>(),
                            itemBindings,
                            modelType
                        );
                    }
                )
                .AddTransient<Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, DetailFormLayout?, string?, IReadOnlyFieldsCollectionBuilder>>
                (
                    provider =>
                    (modelType, fieldSettings, groupBoxSettings, formLayout, parentName) =>
                    {
                        return new ReadOnlyFieldsCollectionBuilder
                        (
                            provider.GetRequiredService<ICollectionBuilderFactory>(),
                            provider.GetRequiredService<IContextProvider>(),
                            provider.GetRequiredService<IDirectiveManagersFactory>(),
                            provider.GetRequiredService<IMapper>(),
                            fieldSettings,
                            groupBoxSettings,
                            modelType,
                            formLayout,
                            parentName
                        );
                    }
                );
        }
    }
}
