using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration;
using Contoso.Parameters.Expressions;
using Contoso.Utils;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class PickerReadOnlyObject<T> : ReadOnlyObjectBase<T>, IHasItemsSourceReadOnly
    {
        public PickerReadOnlyObject(string name, string title, string stringFormat, DropDownTemplateDescriptor dropDownTemplate, IContextProvider contextProvider) : base(name, dropDownTemplate.TemplateName, contextProvider.UiNotificationService)
        {
            this._dropDownTemplate = dropDownTemplate;
            this.httpService = contextProvider.HttpService;
            _defaultTitle = title;
            _stringFormat = stringFormat;
            this.Title = _defaultTitle;
            this.mapper = contextProvider.Mapper;
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;
        private List<object> _items;
        private readonly IMapper mapper;
        private readonly string _defaultTitle;
        private readonly string _stringFormat;

        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

        public string DisplayText
        {
            get
            {
                if (SelectedItem == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(_stringFormat))
                    return SelectedItem.GetPropertyValue<string>(_dropDownTemplate.TextField);

                return string.Format
                (
                    CultureInfo.CurrentCulture,
                    _stringFormat,
                    SelectedItem.GetPropertyValue<string>(_dropDownTemplate.TextField)
                );
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

        public object SelectedItem
        {
            get
            {
                if (_items?.Any() != true)
                    return null;

                return _items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
                    )
                );
            }
        }

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public List<object> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private async void GetItemSource()
        {
            await GetItems(this.DropDownTemplate.TextAndValueSelector);
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(DisplayText));
        }

        private async Task GetItems(SelectorLambdaOperatorDescriptor selector)
        {
            try
            {
                BaseResponse response = await this.httpService.GetObjectDropDown
                (
                    new GetTypedListRequest
                    {
                        DataType = this._dropDownTemplate.RequestDetails.DataType,
                        ModelType = this._dropDownTemplate.RequestDetails.ModelType,
                        ModelReturnType = this._dropDownTemplate.RequestDetails.ModelReturnType,
                        DataReturnType = this._dropDownTemplate.RequestDetails.DataReturnType,
                        Selector = selector
                    },
                    this._dropDownTemplate.RequestDetails.DataSourceUrl
                );

                if (response?.Success != true)
                {
#if DEBUG
                    await App.Current.MainPage.DisplayAlert
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
#endif
                    return;
                }

                Items = ((GetListResponse)response).List.Cast<object>().ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async void Reload(object entity, Type entityType)
        {
            using (IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                flowManagerService.SetFlowDataCacheItem(entityType.FullName, entity);

                await flowManagerService.RunFlow
                (
                    new NewFlowRequest
                    {
                        InitialModuleName = this._dropDownTemplate.ReloadItemsFlowName
                    }
                );

                if ((flowManagerService.GetFlowDataCacheItem($"Get_{this.Name}_Selector_Success") ?? false).Equals(false))
                    return;

                SelectorLambdaOperatorDescriptor selector = this.mapper.Map<SelectorLambdaOperatorDescriptor>
                (
                    flowManagerService.GetFlowDataCacheItem($"{this.Name}_{typeof(SelectorLambdaOperatorParameters).FullName}")
                );

                this.Title = this._dropDownTemplate.LoadingIndicatorText;

                await GetItems(selector);
            }

            this.Title = _defaultTitle;

            Value = GetExistingValue() ?? default;

            T GetExistingValue()
            {
                object existing = Items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
                    )
                );

                return existing == null
                    ? default
                    : existing.GetPropertyValue<T>(_dropDownTemplate.ValueField);
            }
        }

        public override void Clear()
        {
            Items = null;
            Value = default;
        }
    }
}
