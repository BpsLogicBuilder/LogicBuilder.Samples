using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ReadOnlyServices
    {
        internal static IServiceCollection AddReadOnlyServices(this IServiceCollection services)
        {
            return services
                .AddTransient<Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, DetailFormLayout?, string?, IReadOnlyFieldsCollectionBuilder>>
                (
                    provider =>
                    (modelType, fieldSettings, groupBoxSettings, formLayout, parentName) =>
                    {
                        return new ReadOnlyFieldsCollectionBuilder
                        (
                            provider.GetRequiredService<IContextProvider>(),
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
