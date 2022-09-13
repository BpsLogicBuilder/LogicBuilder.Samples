using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ValidatableServices
    {
        internal static IServiceCollection AddValidatableServices(this IServiceCollection services) 
            => services
                .AddTransient<Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IFieldsCollectionBuilder>>
                (
                    provider =>
                    (modelType, fieldSettings, groupBoxSettings, validationMessages, formLayout, parentName) =>
                    {
                        return new FieldsCollectionBuilder
                        (
                            provider.GetRequiredService<IContextProvider>(),
                            fieldSettings,
                            groupBoxSettings,
                            validationMessages,
                            modelType,
                            formLayout,
                            parentName
                        );
                    }
                )
                .AddTransient<Func<Type, List<FormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IUpdateOnlyFieldsCollectionBuilder>>
                (
                    provider =>
                    (modelType, fieldSettings, groupBoxSettings, validationMessages, formLayout, parentName) =>
                    {
                        return new UpdateOnlyFieldsCollectionBuilder
                        (
                            provider.GetRequiredService<IContextProvider>(),
                            fieldSettings,
                            groupBoxSettings,
                            validationMessages,
                            modelType,
                            formLayout,
                            parentName
                        );
                    }
                );
    }
}
