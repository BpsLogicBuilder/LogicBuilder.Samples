using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.Parameters.Expressions;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.SearchPage
{
    public class SearchPageViewModel<TModel> : SearchPageViewModelBase where TModel : Domain.EntityModelBase
    {
        public SearchPageViewModel(
            ICollectionCellManager collectionCellManager,
            IHttpService httpService,
            IMapper mapper,
            ScreenSettings<SearchFormSettingsDescriptor> screenSettings)
            : base(screenSettings)
        {
            itemBindings = FormSettings.Bindings.Values.ToList();
            this.collectionCellManager = collectionCellManager;
            this.httpService = httpService;
            this.mapper = mapper;
            defaultSkip = FormSettings.SortCollection.Skip;
            GetItems();
        }

        private readonly ICollectionCellManager collectionCellManager;
        private readonly IHttpService httpService;
        private readonly IMapper mapper;
        private readonly int? defaultSkip;
        private readonly List<ItemBindingDescriptor> itemBindings;
        private Dictionary<Dictionary<string, IReadOnly>, TModel>? _entitiesDictionary;

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        private string? _searchText;
        public string? SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value)
                    return;

                _searchText = value;
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

        private ICommand? _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand != null)
                    return _addCommand;

                _addCommand = new Command<CommandButtonDescriptor>
                (
                     Add
                );

                return _addCommand;
            }
        }

        private ICommand? _buttonRefreshCommand;
        public ICommand ButtonRefreshCommand
        {
            get
            {
                if (_buttonRefreshCommand != null)
                    return _buttonRefreshCommand;

                _buttonRefreshCommand = new Command
                (
                    () =>
                    {
                        IsRefreshing = true;/*IsRefreshing = true triggers the refresh command.*/
                    }
                );

                return _buttonRefreshCommand;
            }
        }

        private ICommand? _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand != null)
                    return _editCommand;

                _editCommand = new Command<CommandButtonDescriptor>
                (
                    Edit,
                    (button) => SelectedItem != null
                );

                return _editCommand;
            }
        }

        private ICommand? _detailCommand;
        public ICommand DetailCommand
        {
            get
            {
                if (_detailCommand != null)
                    return _detailCommand;

                _detailCommand = new Command<CommandButtonDescriptor>
                (
                    Detail,
                    (button) => SelectedItem != null
                );

                return _detailCommand;
            }
        }

        private ICommand? _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand != null)
                    return _deleteCommand;

                _deleteCommand = new Command<CommandButtonDescriptor>
                (
                    Delete,
                    (button) => SelectedItem != null
                );

                return _deleteCommand;
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

        private ICommand? _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand != null)
                    return _refreshCommand;

                _refreshCommand = new Command
                (
                    PullMoreItems
                );

                return _refreshCommand;
            }
        }

        private ICommand? _textChangedCommand;
        public ICommand TextChangedCommand
        {
            get
            {
                if (_textChangedCommand != null)
                    return _textChangedCommand;

                string? text = null;
                _textChangedCommand = new Command
                (
                    async (parameter) =>
                    {
                        const int debounceDelay = 1000;
                        text = ((TextChangedEventArgs)parameter).NewTextValue;
                        if (text == null)
                            return;

                        await Task.Delay(debounceDelay).ContinueWith
                        (
                            (task, oldText) =>
                            {
                                if (text == (string)oldText!)/*oldText is never null*/
                                    Filter();
                            },
                            text
                        );
                    }
                );

                return _textChangedCommand;
            }
        }

        private ICommand? _selectAndNavigateCommand;
        public ICommand SelectAndNavigateCommand
        {
            get
            {
                if (_selectAndNavigateCommand != null)
                    return _selectAndNavigateCommand;

                _selectAndNavigateCommand = new Command<CommandButtonDescriptor>
                (
                     SelectAndNavigate,
                    (button) => SelectedItem != null
                );

                return _selectAndNavigateCommand;
            }
        }

        private void Filter()
        {
            this.FormSettings.SortCollection.Skip = defaultSkip;
            GetItems();
        }

        private void CheckCanExecute()
        {
            ((Command)EditCommand).ChangeCanExecute();
            ((Command)DeleteCommand).ChangeCanExecute();
            ((Command)DetailCommand).ChangeCanExecute();
            ((Command)SelectAndNavigateCommand).ChangeCanExecute();
        }

        private async Task<BaseResponse> GetList()
        {
            var selector = await GetSelector();

            return await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => this.httpService.GetList
                (
                    new GetTypedListRequest
                    {
                        Selector = selector,
                        ModelType = this.FormSettings.RequestDetails.ModelType,
                        DataType = this.FormSettings.RequestDetails.DataType,
                        ModelReturnType = this.FormSettings.RequestDetails.ModelReturnType,
                        DataReturnType = this.FormSettings.RequestDetails.DataReturnType
                    }
                )
            );
        }

        private async Task<SelectorLambdaOperatorDescriptor> GetSelector()
        {
            using IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.SetFlowDataCacheItem
            (
                FlowDataCacheItemKeys.SkipCount,
                this.FormSettings.SortCollection.Skip.GetValueOrDefault()
            );

            flowManagerService.SetFlowDataCacheItem
            (
                FlowDataCacheItemKeys.SearchText,
                this.SearchText ?? ""
            );

            await flowManagerService.RunFlow
            (
                new NewFlowRequest
                {
                    InitialModuleName = FormSettings.CreatePagingSelectorFlowName
                }
            );

            return this.mapper.Map<SelectorLambdaOperatorDescriptor>
            (
                flowManagerService.GetFlowDataCacheItem(typeof(SelectorLambdaOperatorParameters).FullName!)/*FullName of known type*/
            );
        }

        private async void GetItems()
        {
            BaseResponse baseResponse = await GetList();

            if (baseResponse.Success == false)
                return;

            GetListResponse getListResponse = (GetListResponse)baseResponse;

            this._entitiesDictionary = getListResponse.List.Cast<TModel>().Select
            (
                item => this.collectionCellManager.GetCollectionCellDictionaryModelPair
                (
                    item,
                    this.itemBindings
                )
            ).ToDictionary(k => k.Key, v => v.Value);

            this.Items = new ObservableCollection<Dictionary<string, IReadOnly>>
            (
                this._entitiesDictionary.Keys
            );
        }

        private async void PullMoreItems()
        {
            if (this._entitiesDictionary == null)
                throw new ArgumentException($"{nameof(this._entitiesDictionary)}: {{59B53FDE-2E96-4316-AF1B-E8C8D7FB00AB}}");

            this.FormSettings.SortCollection.Skip = (defaultSkip ?? 0) + this._entitiesDictionary.Count;

            BaseResponse baseResponse = await GetList();
            IsRefreshing = false;

            if (baseResponse.Success == false)
                return;

            if (this._entitiesDictionary == null)
            {
                this._entitiesDictionary = new Dictionary<Dictionary<string, IReadOnly>, TModel>();
                this.Items = new ObservableCollection<Dictionary<string, IReadOnly>>();
            }

            GetListResponse getListResponse = (GetListResponse)baseResponse;

            this._entitiesDictionary = getListResponse.List.Cast<TModel>().Aggregate(this._entitiesDictionary, (list, next) =>
            {
                var kvp = this.collectionCellManager.GetCollectionCellDictionaryModelPair
                (
                    next,
                    this.itemBindings
                );
                list.Add(kvp.Key, kvp.Value);
                (
                    this.Items ?? throw new ArgumentException($"{nameof(this.Items)}: {{E1D5AE89-3351-4E16-942C-24B923AD061D}}")
                ).Add(kvp.Key);

                return list;
            });
        }

        private void SelectAndNavigate(CommandButtonDescriptor button)
        {
            SetEntityAndNavigateNext(button);
        }

        private void Add(CommandButtonDescriptor button)
        {
            SearchPageViewModel<TModel>.NavigateNext(button);
        }

        private void Edit(CommandButtonDescriptor button)
        {
            SetEntityAndNavigateNext(button);
        }

        private void Delete(CommandButtonDescriptor button)
        {
            SetEntityAndNavigateNext(button);
        }

        private void Detail(CommandButtonDescriptor button)
        {
            SetEntityAndNavigateNext(button);
        }

        private void SetEntityAndNavigateNext(CommandButtonDescriptor button)
        {
            using IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.CopyFlowItems();

            if (this.SelectedItem == null)
                throw new ArgumentException($"{nameof(this.SelectedItem)}: {{E28E220D-FBA4-405C-A803-61C5BE385943}}");
            if (this._entitiesDictionary == null)
                throw new ArgumentException($"{nameof(this._entitiesDictionary)}: {{30F5CBC2-1FC0-431E-B326-0642380001E6}}");

            flowManagerService.SetFlowDataCacheItem
            (
                typeof(TModel).FullName!,
                this._entitiesDictionary[SelectedItem]
            );

            flowManagerService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );
        }

        private static Task NavigateNext(CommandButtonDescriptor button)
        {
            using IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.CopyFlowItems();

            return flowManagerService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );
        }
    }
}
