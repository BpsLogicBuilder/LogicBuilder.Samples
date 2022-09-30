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
using Microsoft.Maui.Controls;
using System.Diagnostics.CodeAnalysis;
using Contoso.XPlatform.Constants;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class PickerValidatableObject<T> : ValidatableObjectBase<T>, IHasItemsSourceValidatable
    {
        public PickerValidatableObject(
            IHttpService httpService,
            IMapper mapper,
            UiNotificationService uiNotificationService,
            string name,
            T defaultValue,
            DropDownTemplateDescriptor dropDownTemplate,
            IEnumerable<IValidationRule>? validations)
            : base(name, dropDownTemplate.TemplateName, validations, uiNotificationService)
        {
            this.defaultValue = defaultValue;
            this._dropDownTemplate = dropDownTemplate;
            this.httpService = httpService;
            this.Title = this._dropDownTemplate.LoadingIndicatorText;
            this.mapper = mapper;
            GetItemSource();
        }

        private readonly T defaultValue;
        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;
        private readonly IMapper mapper;

        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

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

        private object? _selectedItem;
        public object? SelectedItem
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

        public override T? Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private List<object>? _items;
        public List<object>? Items
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

                if (response.Success != true)
                {
#if DEBUG
                    await App.Current!.MainPage!.DisplayAlert
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
#endif
                    return;
                }
#if ANDROID
                //This Xamarin.Forms Android issue no longer seems to be a problem in MAUI
                //Items = null;
                //await System.Threading.Tasks.Task.Delay(400);
#endif
#if WINDOWS
                //MAUI bug https://github.com/dotnet/maui/issues/9739
                Items = new List<object>(((GetListResponse)response).List);
#endif
                Items = new List<object>(((GetListResponse)response).List);
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
                flowManagerService.SetFlowDataCacheItem
                (
                    entityType.FullName ?? throw new ArgumentException($"{nameof(entityType.FullName)}: {{65A904DC-79FE-4870-B6DA-EF68600CC7C4}}"), 
                    entity
                );

                await flowManagerService.RunFlow
                (
                    new NewFlowRequest
                    {
                        InitialModuleName = this._dropDownTemplate.ReloadItemsFlowName
                    }
                );

                if ((flowManagerService.GetFlowDataCacheItem(FlowDataCacheItemKeys.Get_Selector_Success) ?? false).Equals(false))
                    return;

                SelectorLambdaOperatorDescriptor selector = this.mapper.Map<SelectorLambdaOperatorDescriptor>
                (
                    flowManagerService.GetFlowDataCacheItem(typeof(SelectorLambdaOperatorParameters).FullName!)/*FullName of known type*/
                );

                this.Title = this._dropDownTemplate.LoadingIndicatorText;

                await GetItems(selector);
            }

            Value = GetExistingValue() ?? this.defaultValue ?? default;

            IsValid = Validate();

            T? GetExistingValue()
            {
                object? existing = Items?.FirstOrDefault
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
