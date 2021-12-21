using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class FormArrayReadOnlyObject<T, E> : ReadOnlyObjectBase<T> where T : ObservableCollection<E> where E : class
    {
        public FormArrayReadOnlyObject(string name, FormGroupArraySettingsDescriptor setting, IContextProvider contextProvider) 
            : base(name, setting.FormGroupTemplate.TemplateName, contextProvider.UiNotificationService)
        {
            this.FormSettings = setting;
            this.formsCollectionDisplayTemplateDescriptor = setting.FormsCollectionDisplayTemplate;
            this.itemBindings = this.formsCollectionDisplayTemplateDescriptor.Bindings.Values.ToList();
            this.Title = this.FormSettings.Title;
            this.Placeholder = this.FormSettings.Placeholder;
            this.contextProvider = contextProvider;
        }

        private readonly IContextProvider contextProvider;
        private readonly FormsCollectionDisplayTemplateDescriptor formsCollectionDisplayTemplateDescriptor;
        private readonly List<ItemBindingDescriptor> itemBindings;
        private Dictionary<Dictionary<string, IReadOnly>, E> _entitiesDictionary;
        public IChildFormGroupSettings FormSettings { get; set; }
        public FormsCollectionDisplayTemplateDescriptor FormsCollectionDisplayTemplate => formsCollectionDisplayTemplateDescriptor;

        public string DisplayText => string.Empty;

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

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder; set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
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
                                new Views.ReadOnlyChildFormArrayPageCS(this)
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
                        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current.MainPage.Navigation.PopModalAsync()
                        );
                    });

                return _cancelCommand;
            }
        }

        private ICommand _detailCommand;
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
            (DetailCommand as Command).ChangeCanExecute();
        }

        private void Detail()
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread
            (
                () => App.Current.MainPage.Navigation.PushModalAsync
                (
                    new Views.ReadOnlyChildFormPageCS
                    (
                        new FormReadOnlyObject<E>
                        (
                            Value.IndexOf(this._entitiesDictionary[this.SelectedItem]).ToString(),
                            this.FormSettings,
                            this.contextProvider
                        )
                        {
                            Value = this._entitiesDictionary[this.SelectedItem]
                        }
                    )
                )
            );
        }
    }
}
