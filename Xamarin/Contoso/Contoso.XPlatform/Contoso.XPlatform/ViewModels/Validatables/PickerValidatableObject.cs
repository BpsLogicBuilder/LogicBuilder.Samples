using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration;
using Contoso.Parameters.Expressions;
using Contoso.Utils;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class PickerValidatableObject<T> : ValidatableObjectBase<T>, IHasItemsSourceValidatable
    {
        public PickerValidatableObject(string name, T defaultValue, DropDownTemplateDescriptor dropDownTemplate, IEnumerable<IValidationRule> validations, IContextProvider contextProvider)
            : base(name, dropDownTemplate.TemplateName, validations, contextProvider.UiNotificationService)
        {
            this.defaultValue = defaultValue;
            this._dropDownTemplate = dropDownTemplate;
            this.httpService = contextProvider.HttpService;
            this.Title = this._dropDownTemplate.LoadingIndicatorText;
            this.mapper = contextProvider.Mapper;
            GetItemSource();
        }

        private T defaultValue;
        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;
        private readonly IMapper mapper;

        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

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

        private object _selectedItem;
        public object SelectedItem
        {
            get
            {
                if (Items?.Any() != true)
                    return null;

                return Items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
                    )
                );
            }

            set
            {
                if (_selectedItem == null && value == null)
                    return;

                if (_selectedItem != null && _selectedItem.Equals(value))
                    return;

                _selectedItem = value;

                if (_selectedItem != null)
                    Value = _selectedItem.GetPropertyValue<T>(_dropDownTemplate.ValueField);

                OnPropertyChanged();
            }
        }

        public override T Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private List<object> _items;
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

                Items = null;
                await System.Threading.Tasks.Task.Delay(400);
                Items = ((GetListResponse)response).List.Cast<object>().ToList();
                OnPropertyChanged(nameof(SelectedItem));

                this.Title = this._dropDownTemplate.TitleText;
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

            Value = GetExistingValue() ?? this.defaultValue ?? default;

            IsValid = Validate();

            T GetExistingValue()
            {
                object existing = Items?.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
                    )
                );

                return existing == null
                    ? this.defaultValue ?? default
                    : existing.GetPropertyValue<T>(_dropDownTemplate.ValueField);
            }
        }

        public override void Clear()
        {
            Items = null;
            Value = this.defaultValue ?? default;
            IsValid = Validate();
        }

        public ICommand SelectedIndexChangedCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
