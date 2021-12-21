using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormValidatableObject<T> : ValidatableObjectBase<T>, IDisposable where T : class
    {
        public FormValidatableObject(string name, IChildFormGroupSettings setting, IEnumerable<IValidationRule> validations, IContextProvider contextProvider) : base(name, setting.FormGroupTemplate.TemplateName, validations, contextProvider.UiNotificationService)
        {
            this.FormSettings = setting;
            this.Title = this.FormSettings.Title;
            this.Placeholder = this.FormSettings.ValidFormControlText;
            this.entityUpdater = contextProvider.EntityUpdater;
            this.propertiesUpdater = contextProvider.PropertiesUpdater;
            this.fieldsCollectionBuilder = contextProvider.FieldsCollectionBuilder;
            this.updateOnlyFieldsCollectionBuilder = contextProvider.UpdateOnlyFieldsCollectionBuilder;
            CreateFieldsCollection();

            this.directiveManagers = new DirectiveManagers<T>
            (
                FormLayout.Properties,
                FormSettings,
                contextProvider
            );

            propertyChangedSubscription = this.uiNotificationService.ValueChanged.Subscribe(FieldChanged);
        }

        protected virtual void CreateFieldsCollection()
        {
            FormLayout = updateOnlyFieldsCollectionBuilder.CreateFieldsCollection(this.FormSettings, typeof(T));
        }

        public event EventHandler Cancelled;
        public event EventHandler Submitted;

        public EditFormLayout FormLayout { get; set; }
        
        public IChildFormGroupSettings FormSettings { get; set; }
        private readonly IEntityUpdater entityUpdater;
        private readonly IPropertiesUpdater propertiesUpdater;
        private readonly IDisposable propertyChangedSubscription;
        private readonly DirectiveManagers<T> directiveManagers;

        protected readonly IFieldsCollectionBuilder fieldsCollectionBuilder;
        private readonly IUpdateOnlyFieldsCollectionBuilder updateOnlyFieldsCollectionBuilder;

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        public string DisplayText => string.Empty;

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder; 
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public override T Value
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

                IsValid = Validate();

                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public ICommand TextChangedCommand => new Command
        (
            (parameter) =>
            {
                IsDirty = true;
                string text = ((TextChangedEventArgs)parameter).NewTextValue;
                if (text == null)
                    return;

                IsValid = Validate();
            }
        );

        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand != null)
                    return _submitCommand;

                _submitCommand = new Command
                (
                    () =>
                    {
                        Value = this.entityUpdater.ToModelObject
                        (
                            FormLayout.Properties, 
                            this.FormSettings.FieldSettings, 
                            Value
                        );

                        Placeholder = IsValid ? this.FormSettings.ValidFormControlText : this.FormSettings.InvalidFormControlText;

                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PopModalAsync()
                        );

                        Submitted?.Invoke(this, new EventArgs());
                    },
                    canExecute: AreFieldsValid
                );

                return _submitCommand;
            }
        }

        private ICommand _openCommand;
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
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PushModalAsync
                            (
                                new Views.ChildFormPageCS(this)
                            )
                        );
                    }
                );

                return _openCommand;
            }
        }

        private ICommand _cancelCommand;
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
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => App.Current.MainPage.Navigation.PopModalAsync()
            );

            Cancelled?.Invoke(this, new EventArgs());
        }

        public virtual void Dispose()
        {
            Dispose(this.directiveManagers);
            Dispose(this.propertyChangedSubscription);
            foreach (var property in FormLayout.Properties)
            {
                if (property is IDisposable disposable)
                    Dispose(disposable);
            }
        }

        public override bool Validate()
        {
            if (!AreFieldsValid())
                Errors = new Dictionary<string, string> { [Name] = this.FormSettings.InvalidFormControlText };

            IsValid = Errors?.Any() != true;

            return this.IsValid;
        }

        protected void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }

        private bool AreFieldsValid()
            => FormLayout.Properties.Aggregate
            (
                true,
                (isTrue, next) => next.Validate() && isTrue
            );

        private void FieldChanged(string fieldName)
        {
            (SubmitCommand as Command).ChangeCanExecute();
        }
    }
}
