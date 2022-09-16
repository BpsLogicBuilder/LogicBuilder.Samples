using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Validators
{
    internal class ReadOnlyDirectiveManagers<TModel> : IReadOnlyDirectiveManagers
    {
        public ReadOnlyDirectiveManagers(IContextProvider contextProvider, IMapper mapper, ObservableCollection<IReadOnly> properties, IFormGroupSettings formSettings)
            : this(properties, formSettings, contextProvider, mapper)
        {
        }

        public ReadOnlyDirectiveManagers(ObservableCollection<IReadOnly> properties, IFormGroupSettings formSettings, IContextProvider contextProvider, IMapper mapper)
        {
            this.properties = properties;
            this.formSettings = formSettings;

            this.hideIfManager = new HideIfManager<TModel>
            (
                this.properties,
                contextProvider.HideIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                ),
                mapper,
                contextProvider.UiNotificationService
            );

            this.clearIfManager = new ClearIfManager<TModel>
            (
                this.properties,
                contextProvider.ClearIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                ),
                mapper,
                contextProvider.UiNotificationService
            );

            this.reloadIfManager = new ReloadIfManager<TModel>
            (
                this.properties,
                contextProvider.ReloadIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                ),
                mapper,
                contextProvider.UiNotificationService
            );
        }

        private readonly ObservableCollection<IReadOnly> properties;
        private readonly IFormGroupSettings formSettings;

        private readonly HideIfManager<TModel> hideIfManager;
        private readonly ClearIfManager<TModel> clearIfManager;
        private readonly ReloadIfManager<TModel> reloadIfManager;

        public void Dispose()
        {
            Dispose(this.hideIfManager);
            Dispose(this.clearIfManager);
            Dispose(this.reloadIfManager);
        }

        private static void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
