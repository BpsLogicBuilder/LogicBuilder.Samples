using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Factories;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormValidatableObject<T> : ValidatableObjectBase<T>, IDisposable where T : class
    {
        public FormValidatableObject(
            ICollectionBuilderFactory collectionBuilderFactory,
            IDirectiveManagersFactory directiveManagersFactory,
            IEntityUpdater entityUpdater,
            IPropertiesUpdater propertiesUpdater,
            UiNotificationService uiNotificationService,
            string name,
            IChildFormGroupSettings setting,
            IEnumerable<IValidationRule>? validations) : base(name, setting.FormGroupTemplate.TemplateName, validations, uiNotificationService)
        {
            this.FormSettings = setting;
            this.Title = this.FormSettings.Title;
            this.Placeholder = this.FormSettings.ValidFormControlText;
            this.entityUpdater = entityUpdater;
            this.propertiesUpdater = propertiesUpdater;
            this.fieldsCollectionBuilder = collectionBuilderFactory.GetFieldsCollectionBuilder
            (
                typeof(T),
                this.FormSettings.FieldSettings,
                this.FormSettings,
                this.FormSettings.ValidationMessages,
                null,
                null
            );

            this.updateOnlyFieldsCollectionBuilder = collectionBuilderFactory.GetUpdateOnlyFieldsCollectionBuilder
            (
                typeof(T),
                this.FormSettings.FieldSettings,
                this.FormSettings,
                this.FormSettings.ValidationMessages,
                null,
                null
            );

            CreateFieldsCollection();

            this.directiveManagers = (DirectiveManagers<T>)directiveManagersFactory.GetDirectiveManagers
            (
                typeof(T),
                FormLayout.Properties,
                FormSettings
            );

            propertyChangedSubscription = this.uiNotificationService.ValueChanged.Subscribe(FieldChanged);
        }

        [MemberNotNull(nameof(FormLayout))]
        protected virtual void CreateFieldsCollection()
        {
            FormLayout = updateOnlyFieldsCollectionBuilder.CreateFields();
        }

        public event EventHandler? Cancelled;
        public event EventHandler? Submitted;

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
            [MemberNotNull(nameof(_title))]
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

        private ICommand? _submitCommand;
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

                        MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current!.MainPage!.Navigation.PopModalAsync()
                        );

                        Submitted?.Invoke(this, new EventArgs());
                    },
                    canExecute: AreFieldsValid
                );

                return _submitCommand;
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
                            () => App.Current!.MainPage!.Navigation.PushModalAsync/*App.Current.MainPage is not null here*/
                            (
                                new Views.ChildFormPageCS(this)
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
            GC.SuppressFinalize(this);
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
            ((Command)SubmitCommand).ChangeCanExecute();
        }
    }
}
