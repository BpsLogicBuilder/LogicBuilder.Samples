using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Validators
{
    internal class ReadOnlyDirectiveManagers<TModel> : IReadOnlyDirectiveManagers
    {
        public ReadOnlyDirectiveManagers(IDirectiveManagersFactory directiveManagersFactory, IContextProvider contextProvider, IMapper mapper, ObservableCollection<IReadOnly> properties, IFormGroupSettings formSettings)
        {
            this.properties = properties;
            this.formSettings = formSettings;

            this.hideIfManager = directiveManagersFactory.GetHideIfManager
            (
                this.properties,
                contextProvider.HideIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                )
            );

            this.clearIfManager = directiveManagersFactory.GetClearIfManager
            (
                this.properties,
                contextProvider.ClearIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                )
            );

            this.reloadIfManager = directiveManagersFactory.GetReloadIfManager
            (
                this.properties,
                contextProvider.ReloadIfConditionalDirectiveBuilder.GetConditions<TModel>
                (
                    this.formSettings,
                    this.properties
                )
            );
        }

        private readonly ObservableCollection<IReadOnly> properties;
        private readonly IFormGroupSettings formSettings;

        private readonly IHideIfManager hideIfManager;
        private readonly IClearIfManager clearIfManager;
        private readonly IReloadIfManager reloadIfManager;

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
