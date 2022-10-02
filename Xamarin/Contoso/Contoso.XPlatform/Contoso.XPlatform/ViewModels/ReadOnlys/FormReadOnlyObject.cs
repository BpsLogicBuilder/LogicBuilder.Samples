using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.Views.Factories;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class FormReadOnlyObject<T> : ReadOnlyObjectBase<T>, IDisposable where T : class
    {
        public FormReadOnlyObject(
            ICollectionBuilderFactory collectionBuilderFactory,
            IDirectiveManagersFactory directiveManagersFactory,
            IPopupFormFactory popupFormFactory,
            IReadOnlyPropertiesUpdater readOnlyPropertiesUpdater,
            IUiNotificationService uiNotificationService,
            string name,
            IChildFormGroupSettings setting) 
            : base(name, setting.FormGroupTemplate.TemplateName, uiNotificationService)
        {
            this.FormSettings = setting;
            /*MemberNotNull unvailable in 2.1*/
            _title = null!;
            _placeholder = null!;
            /*MemberNotNull unavailable in 2.1*/
            this.Title = this.FormSettings.Title;
            this.popupFormFactory = popupFormFactory;
            this.propertiesUpdater = readOnlyPropertiesUpdater;
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
        private readonly IPopupFormFactory popupFormFactory;
        private readonly IReadOnlyPropertiesUpdater propertiesUpdater;
        private readonly ReadOnlyDirectiveManagers<T> directiveManagers;

        public DetailFormLayout FormLayout { get; set; }

        public string DisplayText => string.Empty;

        private string _title;
        public string Title
        {
            get => _title;
            //[MemberNotNull(nameof(_title))]
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
            //[MemberNotNull(nameof(_placeholder))]
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
                                popupFormFactory.CreateReadOnlyChildFormPage(this)
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
