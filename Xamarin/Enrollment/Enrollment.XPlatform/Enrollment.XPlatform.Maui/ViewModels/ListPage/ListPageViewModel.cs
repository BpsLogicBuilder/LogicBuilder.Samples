using Enrollment.Bsl.Business.Requests;
using Enrollment.Bsl.Business.Responses;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.ListForm;
using Enrollment.XPlatform.Flow.Settings.Screen;
using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Enrollment.XPlatform.ViewModels.ListPage
{
    public class ListPageViewModel<TModel> : ListPageViewModelBase where TModel : Domain.EntityModelBase
    {
        public ListPageViewModel(
            ICollectionCellManager collectionCellManager,
            IHttpService httpService,
            ScreenSettings<ListFormSettingsDescriptor> screenSettings) : base(screenSettings)
        {
            itemBindings = FormSettings.Bindings.Values.ToList();
            this.collectionCellManager = collectionCellManager;
            this.httpService = httpService;
            GetItems();
        }

        private readonly ICollectionCellManager collectionCellManager;
        private readonly IHttpService httpService;
        private readonly List<ItemBindingDescriptor> itemBindings;

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

        private Task<BaseResponse> GetList()
            => BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => httpService.GetList
                (
                    new GetTypedListRequest
                    {
                        Selector = this.FormSettings.FieldsSelector,
                        ModelType = this.FormSettings.RequestDetails.ModelType,
                        DataType = this.FormSettings.RequestDetails.DataType,
                        ModelReturnType = this.FormSettings.RequestDetails.ModelReturnType,
                        DataReturnType = this.FormSettings.RequestDetails.DataReturnType
                    }
                )
            );

        private async void GetItems()
        {
            BaseResponse baseResponse = await GetList();

            if (baseResponse.Success == false)
                return;

            GetListResponse getListResponse = (GetListResponse)baseResponse;
            this.Items = new ObservableCollection<Dictionary<string, IReadOnly>>
            (
                getListResponse.List.Cast<TModel>().Select
                (
                    item => this.collectionCellManager.GetCollectionCellDictionaryItem(item, itemBindings)
                )
            );
        }
    }
}
