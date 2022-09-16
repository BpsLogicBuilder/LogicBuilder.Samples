using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using System.Diagnostics.CodeAnalysis;
using Contoso.XPlatform.ViewModels.Factories;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class FormReadOnlyObject<T> : ReadOnlyObjectBase<T>, IDisposable where T : class
    {
        public FormReadOnlyObject(
            ICollectionBuilderFactory collectionBuilderFactory,
            IContextProvider contextProvider,
            IDirectiveManagersFactory directiveManagersFactory,
            string name,
            IChildFormGroupSettings setting) 
            : base(name, setting.FormGroupTemplate.TemplateName, contextProvider.UiNotificationService)
        {
            this.FormSettings = setting;
            this.Title = this.FormSettings.Title;
            this.propertiesUpdater = contextProvider.ReadOnlyPropertiesUpdater;
            this.Placeholder = this.FormSettings.Placeholder;
            FormLayout = collectionBuilderFactory.GetReadOnlyFieldsCollectionBuilder
            (
                typeof(T),
                this.FormSettings.FieldSettings,
                this.FormSettings,
                null,
                null
            ).CreateFields();

            this.directiveManagers = (ReadOnlyDirectiveManagers<T>)directiveManagersFactory.GetReadOnlyDirectiveManagers
            (
                typeof(T),
                FormLayout.Properties,
                FormSettings
            );
        }

        public IChildFormGroupSettings FormSettings { get; set; }
        private readonly IReadOnlyPropertiesUpdater propertiesUpdater;
        private readonly ReadOnlyDirectiveManagers<T> directiveManagers;

        public DetailFormLayout FormLayout { get; set; }

        public string DisplayText => string.Empty;

        private string _title;
        public string Title
        {
            get => _title;
            [MemberNotNull(nameof(_title))]
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder;
            [MemberNotNull(nameof(_placeholder))]
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public override T? Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                this.propertiesUpdater.UpdateProperties
                (
                    FormLayout.Properties,
                    base.Value,
                    FormSettings.FieldSettings
                );
            }
        }

        private ICommand? _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand != null)
                    return _openCommand;

                _openCommand = new Command
                (
                    () =>
                    {
                        MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current!.MainPage!.Navigation.PushModalAsync/*App.Current.MainPage is not null at this point*/
                            (
                                new Views.ReadOnlyChildFormPageCS(this)
                            )
                        );
                    }
                );

                return _openCommand;
            }
        }

        private ICommand? _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand != null)
                    return _cancelCommand;

                _cancelCommand = new Command
                (
                    Cancel
                );

                return _cancelCommand;
            }
        }

        protected virtual void Cancel()
        {
            MainThread.BeginInvokeOnMainThread
            (
                () => App.Current!.MainPage!.Navigation.PopModalAsync()
            );
        }

        public virtual void Dispose()
        {
            Dispose(this.directiveManagers);
            foreach (var property in FormLayout.Properties)
            {
                if (property is IDisposable disposable)
                    Dispose(disposable);
            }
            GC.SuppressFinalize(this);
        }

        protected void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
