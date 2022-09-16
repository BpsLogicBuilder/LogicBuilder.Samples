using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Factories;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class FormArrayReadOnlyObject<T, E> : ReadOnlyObjectBase<T> where T : ObservableCollection<E> where E : class
    {
        public FormArrayReadOnlyObject(
            ICollectionBuilderFactory collectionBuilderFactory,
            IContextProvider contextProvider,
            IDirectiveManagersFactory directiveManagersFactory,
            string name,
            FormGroupArraySettingsDescriptor setting) 
            : base(name, setting.FormGroupTemplate.TemplateName, contextProvider.UiNotificationService)
        {
            this.FormSettings = setting;
            this.formsCollectionDisplayTemplateDescriptor = setting.FormsCollectionDisplayTemplate;
            this.itemBindings = this.formsCollectionDisplayTemplateDescriptor.Bindings.Values.ToList();
            this.Title = this.FormSettings.Title;
            this.Placeholder = this.FormSettings.Placeholder;
            this.collectionBuilderFactory = collectionBuilderFactory;
            this.contextProvider = contextProvider;
            this.directiveManagersFactory = directiveManagersFactory;
        }

        private readonly ICollectionBuilderFactory collectionBuilderFactory;
        private readonly IContextProvider contextProvider;
        private readonly IDirectiveManagersFactory directiveManagersFactory;
        private readonly FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
        private readonly List<ItemBindingDescriptor> itemBindings;
        private Dictionary<Dictionary<string, IReadOnly>, E>? _entitiesDictionary;
        public IChildFormGroupSettings FormSettings { get; set; }
        public FormsCollectionDisplayTemplateDescriptor FormsCollectionDisplayTemplate => formsCollectionDisplayTemplateDescriptor;

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

                this._entitiesDictionary = base.Value?.Select
                (
                    item => item.GetCollectionCellDictionaryModelPair
                    (
                        this.collectionBuilderFactory,
                        this.contextProvider,
                        this.itemBindings
                    )
                ).ToDictionary(k => k.Key, v => v.Value) ?? new Dictionary<Dictionary<string, IReadOnly>, E>();

                this.Items = new ObservableCollection<Dictionary<string, IReadOnly>>
                (
                    this._entitiesDictionary.Keys
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
                                new Views.ReadOnlyChildFormArrayPageCS(this)
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
                        MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current!.MainPage!.Navigation.PopModalAsync()/*App.Current.MainPage is not null at this point*/
                        );
                    });

                return _cancelCommand;
            }
        }

        private ICommand? _detailCommand;
        public ICommand DetailCommand
        {
            get
            {
                if (_detailCommand != null)
                    return _detailCommand;

                _detailCommand = new Command
                (
                    Detail,
                    () => SelectedItem != null
                );

                return _detailCommand;
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
            ((Command)DetailCommand).ChangeCanExecute();
        }

        private void Detail()
        {
            MainThread.BeginInvokeOnMainThread
            (
                () =>
                {
                    if (this.Value == null)
                        throw new ArgumentException($"{nameof(this.Value)}: {{A780FCF5-993A-4D2B-91D8-6854AFD70547}}");
                    if (this.SelectedItem == null)
                        throw new ArgumentException($"{nameof(this.SelectedItem)}: {{A6846EA8-B79F-4694-A283-1F647A0C9375}}");
                    if (this._entitiesDictionary == null)
                        throw new ArgumentException($"{nameof(this._entitiesDictionary)}: {{BCD0ED11-4BBE-4570-BA68-857B918AF4F8}}");

                    App.Current!.MainPage!.Navigation.PushModalAsync
                    (
                        new Views.ReadOnlyChildFormPageCS
                        (
                            new FormReadOnlyObject<E>
                            (
                                this.collectionBuilderFactory,
                                this.contextProvider,
                                this.directiveManagersFactory,
                                Value.IndexOf(this._entitiesDictionary[this.SelectedItem]).ToString(),
                                this.FormSettings
                            )
                            {
                                Value = this._entitiesDictionary[this.SelectedItem]
                            }
                        )
                    );
                });
        }
    }
}
