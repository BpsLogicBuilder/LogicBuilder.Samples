using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables.Factories;
using Contoso.XPlatform.Views.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormArrayValidatableObject<T, E> : ValidatableObjectBase<T> where T : ObservableCollection<E> where E : class
    {
        public FormArrayValidatableObject(
            ICollectionCellManager collectionCellManager,
            ICollectionBuilderFactory collectionBuilderFactory,
            IPopupFormFactory popupFormFactory,
            IValidatableFactory validatableFactory,
            IUiNotificationService uiNotificationService,
            string name,
            FormGroupArraySettingsDescriptor setting,
            IEnumerable<IValidationRule>? validations)
            : base(name, setting.FormGroupTemplate.TemplateName, validations, uiNotificationService)
        {
            this.FormSettings = setting;
            this.formsCollectionDisplayTemplateDescriptor = setting.FormsCollectionDisplayTemplate;
            this.itemBindings = this.formsCollectionDisplayTemplateDescriptor.Bindings.Values.ToList();
            /*MemberNotNull unvailable in 2.1*/
            _title = null!;
            _placeholder = null!;
            /*MemberNotNull unavailable in 2.1*/
            this.Title = this.FormSettings.Title;
            this.Placeholder = setting.Placeholder;
            this.collectionCellManager = collectionCellManager;
            this.collectionBuilderFactory = collectionBuilderFactory;
            this.popupFormFactory = popupFormFactory;
            this.validatableFactory = validatableFactory;
            Value = (T)new ObservableCollection<E>();
        }

        private T? _initialValue;
        private readonly ICollectionCellManager collectionCellManager;
        private readonly ICollectionBuilderFactory collectionBuilderFactory;
        private readonly IPopupFormFactory popupFormFactory;
        private readonly IValidatableFactory validatableFactory;
        private readonly FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
        private readonly List<ItemBindingDescriptor> itemBindings;
        private Dictionary<Dictionary<string, IReadOnly>, E>? _entitiesDictionary;
        public IChildFormGroupSettings FormSettings { get; set; }
        public FormsCollectionDisplayTemplateDescriptor FormsCollectionDisplayTemplate => formsCollectionDisplayTemplateDescriptor;

        public string DisplayText => string.Empty;

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

        private Dictionary<string, IReadOnly>? _selectedItem;
        public Dictionary<string, IReadOnly>? SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem == null || !_selectedItem.Equals(value))
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                    CheckCanExecute();
                }
            }
        }

        private ObservableCollection<Dictionary<string, IReadOnly>>? _items;
        public ObservableCollection<Dictionary<string, IReadOnly>>? Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public override T? Value
        {
            get => base.Value;
            set
            {
                base.Value = value;

                _initialValue = (T)new ObservableCollection<E>(value ?? (IEnumerable<E>)new List<E>());

                this._entitiesDictionary = base.Value?.Select
                (
                    item => this.collectionCellManager.GetCollectionCellDictionaryModelPair
                    (
                        item,
                        this.itemBindings
                    )
                ).ToDictionary(k => k.Key, v => v.Value) ?? new Dictionary<Dictionary<string, IReadOnly>, E>();

                this.Items = new ObservableCollection<Dictionary<string, IReadOnly>>
                (
                    this._entitiesDictionary.Keys
                );
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
                        MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current!.MainPage!.Navigation.PopModalAsync()
                        );
                    },
                    () => IsValid
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
                            () => App.Current!.MainPage!.Navigation.PushModalAsync
                            (
                                popupFormFactory.CreateChildFormArrayPage(this)
                            )
                        );
                    });

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
                    () =>
                    {
                        Value = _initialValue;
                        MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current!.MainPage!.Navigation.PopModalAsync()
                        );
                    });

                return _cancelCommand;
            }
        }

        private ICommand? _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand != null)
                    return _editCommand;

                _editCommand = new Command
                (
                    Edit,
                    () => SelectedItem != null
                );

                return _editCommand;
            }
        }

        private ICommand? _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand != null)
                    return _deleteCommand;

                _deleteCommand = new Command
                (
                    () =>
                    {
                        if (this.SelectedItem == null)
                            throw new ArgumentException($"{nameof(this.SelectedItem)}: {{595E81EA-C8CA-4B7A-94A0-BBE4FC75DE6B}}");

                        if (this._entitiesDictionary == null)
                            throw new ArgumentException($"{nameof(this._entitiesDictionary)}: {{7EC9A949-F557-41D4-9425-33370B1EED70}}");

                        RemoveItem(this._entitiesDictionary[this.SelectedItem]);
                    },
                    () => SelectedItem != null
                );

                return _deleteCommand;
            }
        }

        private ICommand? _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand != null)
                    return _addCommand;

                _addCommand = new Command
                (
                     Add
                );

                return _addCommand;
            }
        }

        private ICommand? _selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (_selectionChangedCommand != null)
                    return _selectionChangedCommand;

                _selectionChangedCommand = new Command
                (
                    CheckCanExecute
                );

                return _selectionChangedCommand;
            }
        }

        private void CheckCanExecute()
        {
            ((Command)EditCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();
        }

        private void Edit()
        {
            if (this.Value == null)
                throw new ArgumentException($"{nameof(this.Value)}: {{EE38D992-6D3F-4179-AA2F-AE5237AFAE83}}");
            if (this.SelectedItem == null)
                throw new ArgumentException($"{nameof(this.SelectedItem)}: {{55264B58-1859-47D5-9B5E-53CAA8A91FD5}}");
            if (this._entitiesDictionary == null)
                throw new ArgumentException($"{nameof(this._entitiesDictionary)}: {{824A0081-0E2F-4929-8BF2-C1F65D54AC6D}}");

            var formValidatable = (FormValidatableObject<E>)validatableFactory.CreateFormValidatableObject
            (
                typeof(E),
                Value.IndexOf(this._entitiesDictionary[this.SelectedItem]).ToString(),
                nameof(FormValidatableObject<E>),
                this.FormSettings,
                Array.Empty<IValidationRule>()
            );
            formValidatable.Value = this._entitiesDictionary[this.SelectedItem];

            formValidatable.Cancelled += FormValidatable_Cancelled;
            formValidatable.Submitted += FormValidatable_Submitted;

            MainThread.BeginInvokeOnMainThread
            (
                () => App.Current!.MainPage!.Navigation.PushModalAsync
                (
                    popupFormFactory.CreateChildFormPage(formValidatable)
                )
            );
        }

        private void Add()
        { 
            var newItemPair = collectionCellManager.GetCollectionCellDictionaryModelPair
            (
                Activator.CreateInstance<E>(),
                this.itemBindings
            );

            if (this.Value == null)
                throw new ArgumentException($"{nameof(this.Value)}: {{5BFF71EE-9520-48A1-A341-FAE5E1D7BD72}}");
            if (this.Items == null)
                throw new ArgumentException($"{nameof(this.Items)}: {{6D8AAA37-D13F-46BD-BABD-A4BAF1D82067}}");
            if (this._entitiesDictionary == null)
                throw new ArgumentException($"{nameof(this._entitiesDictionary)}: {{0A1BFB8B-7E3B-4824-8C11-80EB5F4DB873}}");

            Value.Add(newItemPair.Value);
            _entitiesDictionary.Add(newItemPair.Key, newItemPair.Value);
            Items.Add(newItemPair.Key);

            SelectedItem = newItemPair.Key;

            var addValidatable = (AddFormValidatableObject<E>)validatableFactory.CreateFormValidatableObject
            (
                typeof(E),
                Value.Count.ToString(),
                nameof(AddFormValidatableObject<E>),
                this.FormSettings,
                Array.Empty<IValidationRule>()
            );
            addValidatable.Value = newItemPair.Value;

            addValidatable.AddCancelled += AddValidatable_AddCancelled;
            addValidatable.Cancelled += FormValidatable_Cancelled;
            addValidatable.Submitted += FormValidatable_Submitted;

            MainThread.BeginInvokeOnMainThread
            (
                () => App.Current!.MainPage!.Navigation.PushModalAsync
                (
                    popupFormFactory.CreateChildFormPage(addValidatable)
                )
            );
        }

        private void RemoveItem(E entity)
        {
            if (this.Value == null)
                throw new ArgumentException($"{nameof(this.Value)}: {{E6C62407-D366-42BE-840C-CC35B537C43E}}");
            if (this.Items == null)
                throw new ArgumentException($"{nameof(this.Items)}: {{B86E72BB-293A-4D83-87CA-8D9CCA5FC662}}");
            if (this._entitiesDictionary == null)
                throw new ArgumentException($"{nameof(this._entitiesDictionary)}: {{5E433F9D-25BD-4898-95F4-138E10C400C9}}");

            var kvp = _entitiesDictionary.Single(item => object.ReferenceEquals(item.Value, entity));

            if (Value.Remove(kvp.Value) == false)
                throw new InvalidOperationException("{0DA2B9A8-65D5-47BC-9FAA-202341DE9C0E}");

            if (Items.Remove(kvp.Key) == false)
                throw new InvalidOperationException("{A3C2DCA7-398F-4835-971E-A48C5D1D9F7D}");

            if (_entitiesDictionary.Remove(kvp.Key) == false)
                throw new InvalidOperationException("{8A8C2E06-89D7-49B3-83AB-9357CB12C3E3}");

            SelectedItem = null;
        }

        private void AddValidatable_AddCancelled(object? sender, EventArgs e)
        {
            E? valueToRemove = ((AddFormValidatableObject<E>)sender!).Value;
            if (valueToRemove == null)
                throw new ArgumentException($"{nameof(valueToRemove)}: {{09638B91-8F5E-422B-A8E0-E018F9F4928A}}");
            RemoveItem(valueToRemove);
        }

        private void FormValidatable_Cancelled(object? sender, EventArgs e)
        {
            ((FormValidatableObject<E>)sender!).Dispose();
        }

        private void FormValidatable_Submitted(object? sender, EventArgs e)
        {
            if (sender == null)
                throw new ArgumentException($"{nameof(sender)}: {{DE2ADB2B-E251-4D11-BAB4-BDC3F6ACE0D5}}");
            if (this._entitiesDictionary == null)
                throw new ArgumentException($"{nameof(this._entitiesDictionary)}: {{7363895F-65F5-4660-9510-5CEA8C9DE4C7}}");

            var kvp = _entitiesDictionary.Single(item => object.ReferenceEquals(item.Value, ((FormValidatableObject<E>)sender!).Value));

            collectionCellManager.UpdateCollectionCellProperties(kvp.Value, kvp.Key.Values, itemBindings);

            ((FormValidatableObject<E>)sender).Dispose();
        }
    }
}
