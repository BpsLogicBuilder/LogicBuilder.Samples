using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormArrayValidatableObject<T, E> : ValidatableObjectBase<T> where T : ObservableCollection<E> where E : class
    {
        public FormArrayValidatableObject(string name, FormGroupArraySettingsDescriptor setting, IEnumerable<IValidationRule> validations, IContextProvider contextProvider)
            : base(name, setting.FormGroupTemplate.TemplateName, validations, contextProvider.UiNotificationService)
        {
            this.FormSettings = setting;
            this.formsCollectionDisplayTemplateDescriptor = setting.FormsCollectionDisplayTemplate;
            this.itemBindings = this.formsCollectionDisplayTemplateDescriptor.Bindings.Values.ToList();
            this.Title = this.FormSettings.Title;
            this.Placeholder = setting.Placeholder;
            this.contextProvider = contextProvider;
            Value = (T)new ObservableCollection<E>();
        }

        private T _initialValue;
        private readonly IContextProvider contextProvider;
        private readonly FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
        private readonly List<ItemBindingDescriptor> itemBindings;
        private Dictionary<Dictionary<string, IReadOnly>, E> _entitiesDictionary;
        public IChildFormGroupSettings FormSettings { get; set; }
        public FormsCollectionDisplayTemplateDescriptor FormsCollectionDisplayTemplate => formsCollectionDisplayTemplateDescriptor;

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

        private Dictionary<string, IReadOnly> _selectedItem;
        public Dictionary<string, IReadOnly> SelectedItem
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

        private ObservableCollection<Dictionary<string, IReadOnly>> _items;
        public ObservableCollection<Dictionary<string, IReadOnly>> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public override T Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                _initialValue = (T)new ObservableCollection<E>(value);

                this._entitiesDictionary = base.Value.Select
                (
                    item => item.GetCollectionCellDictionaryModelPair
                    (
                        this.contextProvider,
                        this.itemBindings
                    )
                ).ToDictionary(k => k.Key, v => v.Value);

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
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PopModalAsync()
                        );
                    },
                    () => IsValid
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
                                new Views.ChildFormArrayPageCS(this)
                            )
                        );
                    });

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
                    () =>
                    {
                        Value = _initialValue;
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PopModalAsync()
                        );
                    });

                return _cancelCommand;
            }
        }

        private ICommand _editCommand;
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

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand != null)
                    return _deleteCommand;

                _deleteCommand = new Command
                (
                    () => RemoveItem(this._entitiesDictionary[this.SelectedItem]),
                    () => SelectedItem != null
                );

                return _deleteCommand;
            }
        }

        private ICommand _addCommand;
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

        private ICommand _selectionChangedCommand;
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
            (EditCommand as Command).ChangeCanExecute();
            (DeleteCommand as Command).ChangeCanExecute();
        }

        private void Edit()
        {
            var formValidatable = new FormValidatableObject<E>
            (
                Value.IndexOf(this._entitiesDictionary[this.SelectedItem]).ToString(),
                this.FormSettings,
                new IValidationRule[] { },
                this.contextProvider
            )
            {
                Value = this._entitiesDictionary[this.SelectedItem]
            };

            formValidatable.Cancelled += FormValidatable_Cancelled;
            formValidatable.Submitted += FormValidatable_Submitted;

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => App.Current.MainPage.Navigation.PushModalAsync
                (
                    new Views.ChildFormPageCS(formValidatable)
                )
            );
        }

        private void Add()
        {
            var newItemPair = Activator.CreateInstance<E>().GetCollectionCellDictionaryModelPair
            (
                this.contextProvider,
                this.itemBindings
            );

            Value.Add(newItemPair.Value);
            _entitiesDictionary.Add(newItemPair.Key, newItemPair.Value);
            Items.Add(newItemPair.Key);

            SelectedItem = newItemPair.Key;

            var addValidatable = new AddFormValidatableObject<E>
            (
                Value.Count.ToString(),
                this.FormSettings,
                new IValidationRule[] { },
                this.contextProvider
            )
            {
                Value = newItemPair.Value
            };

            addValidatable.AddCancelled += AddValidatable_AddCancelled;
            addValidatable.Cancelled += FormValidatable_Cancelled;
            addValidatable.Submitted += FormValidatable_Submitted;

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => App.Current.MainPage.Navigation.PushModalAsync
                (
                    new Views.ChildFormPageCS(addValidatable)
                )
            );
        }

        private void RemoveItem(E entity)
        {
            var kvp = _entitiesDictionary.Single(item => object.ReferenceEquals(item.Value, entity));

            if (Value.Remove(kvp.Value) == false)
                throw new InvalidOperationException("{0DA2B9A8-65D5-47BC-9FAA-202341DE9C0E}");

            if (Items.Remove(kvp.Key) == false)
                throw new InvalidOperationException("{A3C2DCA7-398F-4835-971E-A48C5D1D9F7D}");

            if (_entitiesDictionary.Remove(kvp.Key) == false)
                throw new InvalidOperationException("{8A8C2E06-89D7-49B3-83AB-9357CB12C3E3}");

            SelectedItem = null;
        }

        private void AddValidatable_AddCancelled(object sender, EventArgs e)
        {
            RemoveItem(((AddFormValidatableObject<E>)sender).Value);
        }

        private void FormValidatable_Cancelled(object sender, EventArgs e)
        {
            ((FormValidatableObject<E>)sender).Dispose();
        }

        private void FormValidatable_Submitted(object sender, EventArgs e)
        {
            var kvp = _entitiesDictionary.Single(item => object.ReferenceEquals(item.Value, ((FormValidatableObject<E>)sender).Value));

            kvp.Value.UpdateCollectionCellProperties(kvp.Key.Values, this.contextProvider, itemBindings);

            ((FormValidatableObject<E>)sender).Dispose();
        }
    }
}
