using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.ViewModels.ReadOnlys.Factories;
using Enrollment.XPlatform.Views.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class FormArrayReadOnlyObject<T, E> : ReadOnlyObjectBase<T> where T : ObservableCollection<E> where E : class
    {
        public FormArrayReadOnlyObject(
            ICollectionCellManager collectionCellManager,
            IPopupFormFactory popupFormFactory,
            IReadOnlyFactory readOnlyFactory,
            IUiNotificationService uiNotificationService,
            string name,
            FormGroupArraySettingsDescriptor setting) 
            : base(name, setting.FormGroupTemplate.TemplateName, uiNotificationService)
        {
            this.FormSettings = setting;
            this.formsCollectionDisplayTemplateDescriptor = setting.FormsCollectionDisplayTemplate;
            this.itemBindings = this.formsCollectionDisplayTemplateDescriptor.Bindings.Values.ToList();

            /*MemberNotNull unvailable in 2.1*/
            _title = null!;
            _placeholder = null!;
            /*MemberNotNull unavailable in 2.1*/
            this.Title = this.FormSettings.Title;
            this.Placeholder = this.FormSettings.Placeholder;
            this.collectionCellManager = collectionCellManager;
            this.popupFormFactory = popupFormFactory;
            this.readOnlyFactory = readOnlyFactory;
        }

        private readonly ICollectionCellManager collectionCellManager;
        private readonly IPopupFormFactory popupFormFactory;
        private readonly IReadOnlyFactory readOnlyFactory;
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
                                popupFormFactory.CreateReadOnlyChildFormArrayPage(this)
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

                    var formValidatable = this.readOnlyFactory.CreateFormReadOnlyObject
                    (
                        typeof(E),
                        Value.IndexOf(this._entitiesDictionary[this.SelectedItem]).ToString(),
                        this.FormSettings
                    );
                    formValidatable.Value = this._entitiesDictionary[this.SelectedItem];

                    App.Current!.MainPage!.Navigation.PushModalAsync
                    (
                        popupFormFactory.CreateReadOnlyChildFormPage
                        (
                            formValidatable
                        )
                    );
                });
        }
    }
}
